﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userControl : MonoBehaviour {
    public OVRInput.Controller controller;
    public GameObject rightHand;
    public GameObject leftHand;

    public GameObject tracking;

    bool leftGrab;
    bool rightGrab;
    int priorityGrab;

    Vector3 currentLeft;
    Vector3 lastLeft;
    Vector3 currentRight;
    Vector3 lastRight;

    Vector3 difference;

    //selection variables
    public float grabRadius;
    public LayerMask grabMask;

    // Use this for initialization
    void Start () {
        leftGrab = false;
        rightGrab = false;
        priorityGrab = 0;

        tracking.transform.Translate(new Vector3(0, 0.25f, 0));
        
	}
	
	// Update is called once per frame
	void Update () {
        currentLeft = leftHand.transform.position;
        currentRight = rightHand.transform.position;
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.9) {
            if (!leftGrab) {
                onLeftGrabbed();
            } else {
                onLeftHold();
            }
        } else {
            leftGrab = false;
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, controller) > 0.9) {
            if (!rightGrab) {
                onRightGrabbed();
            } else {
                onRightHold();
            }
        } else {
            rightGrab = false;
        }
        
        grabSelect();
    }

    void onLeftGrabbed() {
        leftGrab = true;
        priorityGrab = -1;
        lastLeft = leftHand.transform.position;
    }
    void onLeftHold() {
        if (priorityGrab == -1) {
            difference = new Vector3(lastLeft.x - currentLeft.x, 0, lastLeft.z - currentLeft.z);
            this.transform.Translate(difference * 5, null);
        }
        lastLeft = leftHand.transform.position;
    }
    void onRightGrabbed() {
        rightGrab = true;
        priorityGrab = 1;
        lastRight = rightHand.transform.position;
    }
    void onRightHold() {
        if (priorityGrab == 1) {
            difference = new Vector3(lastRight.x - currentRight.x, 0, lastRight.z - currentRight.z);
            this.transform.Translate(difference * 5, null);
        }
        lastRight = rightHand.transform.position;
    }

    private void OnCollisionEnter(Collision collision){
        string hitTag = collision.gameObject.tag;
        if(hitTag == "wall" || hitTag == "fixed"){
            //undo movement
            priorityGrab = 0;
            this.transform.Translate(difference * -5, null);
        }
        
            
    }

    //Virtual Hand select method
    void grabSelect()
    {
        GameObject selection=null;
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(rightHand.transform.position, grabRadius, rightHand.transform.forward, 0f, grabMask);
        if (hits.Length > 0)
        {
            print("hit");
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance)
                {
                    closestHit = i;
                }
            }
            if (selection == null && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, controller) > 0.9 )
            {
                print("pulled trigger");
                selection = hits[closestHit].transform.gameObject;
                selection.GetComponent<Rigidbody>().isKinematic = true;
                selection.transform.position = rightHand.transform.position;
                selection.transform.parent = rightHand.transform;
            }
        }

        //the code for throwing objects
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, controller) < 0.1)
        {
            if (selection != null)
            {
                selection.transform.parent = null;
                selection.GetComponent<Rigidbody>().isKinematic = false;
                selection.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTrackedRemote);
                selection = null;

            }
        }
    }
}
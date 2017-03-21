using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userControl : MonoBehaviour {
    public OVRInput.Controller controller;
    public GameObject rightHand;
    public GameObject leftHand;

    public GameObject camera;

    public GameObject tracking;

    public AudioClip bumpSound;
    public AudioClip jumpSound;



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
    GameObject RTselection;
    GameObject LTselection;

    //input variables
    float jumpingOffset;
    float dragMultiplier;

    // Use this for initialization
    void Start () {
        leftGrab = false;
        rightGrab = false;
        priorityGrab = 0;
        RTselection = null;
        LTselection = null;
        jumpingOffset = 0.6f;
        dragMultiplier = 8;

        tracking.transform.Translate(new Vector3(0, 0.25f, 0));
        
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody bodyRB = GetComponent<Rigidbody>();
        currentLeft = leftHand.transform.position;
        currentRight = rightHand.transform.position;
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.9) {
            if (!leftGrab && bodyRB.velocity.y == 0) {
                onLeftGrabbed();
            } else {
                onLeftHold();
            }
        } else {
            leftGrab = false;
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, controller) > 0.9) {
            if (!rightGrab && bodyRB.velocity.y == 0) {
                onRightGrabbed();
            } else {
                onRightHold();
            }
        } else {
            rightGrab = false;
        }
        //jumping
        if (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).y > jumpingOffset && 
            OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).y > jumpingOffset &&
            !leftGrab && !rightGrab && bodyRB.velocity.y == 0) {
            Vector3 direction = rightHand.transform.position+leftHand.transform.position-2*camera.transform.position;
            bodyRB.velocity = new Vector3(direction.x*10, 6, direction.z*10);
            priorityGrab = 0;
            difference = new Vector3(0, 0, 0);
            sounds.playAudioEffect(jumpSound);
        }
        //grabbing function
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
            this.transform.Translate(difference * dragMultiplier, null);
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
            this.transform.Translate(difference * dragMultiplier, null);
        }
        lastRight = rightHand.transform.position;
    }

    private void OnCollisionEnter(Collision collision){
        string hitTag = collision.gameObject.tag;
        if(hitTag == "wall" || hitTag == "fixed"){

            Rigidbody bodyRB = GetComponent<Rigidbody>();
            if (bodyRB.velocity.y != 0){
                bodyRB.velocity = new Vector3(-bodyRB.velocity.x, bodyRB.velocity.y, -bodyRB.velocity.z);
            } else {
                priorityGrab = 0;
                this.transform.Translate(difference * -dragMultiplier, null); //undo drag movement
            }
        }
        sounds.playAudioEffect(bumpSound);
    }

    //Virtual Hand select method
    void grabSelect(){
        //for right hand
        RaycastHit[] rightHits;
        
        rightHits = Physics.SphereCastAll(rightHand.transform.position, grabRadius, rightHand.transform.forward, 0f, grabMask);
        if (rightHits.Length > 0){
            int closestHit = 0;
            for (int i = 0; i < rightHits.Length; i++){
                if (rightHits[i].distance < rightHits[closestHit].distance){
                    closestHit = i;
                }
            }
            if (RTselection == null && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, controller) > 0.9 ){
                //this will keep the grabbed object bumping into the cat, preventing cat from moving TODO not working correctly
                Vector3 objToHandOffset = new Vector3(0.0f, 0.1f, 0.05f);
                //print("pulled trigger");
                RTselection = rightHits[closestHit].transform.gameObject;
                RTselection.GetComponent<Rigidbody>().isKinematic = true;
                RTselection.transform.position = RTselection.transform.TransformPoint(rightHand.transform.localPosition + objToHandOffset);
                RTselection.transform.parent = rightHand.transform;
            }
        }

        //the code for throwing objects
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, controller) < 0.1){
            if (RTselection != null){
                RTselection.transform.parent = null;
                RTselection.GetComponent<Rigidbody>().isKinematic = false;
                RTselection.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
                RTselection = null;

            }
        }


        //for left hand
        RaycastHit[] leftHits;
        leftHits = Physics.SphereCastAll(leftHand.transform.position, grabRadius, leftHand.transform.forward, 0f, grabMask);
        if (leftHits.Length > 0){
            int closestHit = 0;
            for (int i = 0; i < leftHits.Length; i++){
                if (leftHits[i].distance < leftHits[closestHit].distance){
                    closestHit = i;
                }
            }
            if (LTselection == null && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0.9){
                //print("pulled trigger");
                LTselection = rightHits[closestHit].transform.gameObject;
                LTselection.GetComponent<Rigidbody>().isKinematic = true;
                LTselection.transform.position = leftHand.transform.position;
                LTselection.transform.parent = leftHand.transform;
            }
        }

        //the code for throwing objects
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, controller) < 0.1){
            if (LTselection != null){
                LTselection.transform.parent = null;
                LTselection.GetComponent<Rigidbody>().isKinematic = false;
                LTselection.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
                LTselection = null;

            }
        }
    }
}
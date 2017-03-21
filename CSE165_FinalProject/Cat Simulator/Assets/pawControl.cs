using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawControl : MonoBehaviour {
    public OVRInput.Controller controller;
    public bool left;
    public bool right;
    public float positionOffset;

    public GameObject map;
    public GameObject map2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (left){
            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
        else if(right){
            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        }

    }
    private void OnCollisionEnter(Collision collision){
        
        string hitTag=collision.gameObject.tag;
        if (hitTag == "showmap"){
            map.GetComponent<MeshRenderer>().enabled = true;
            map2.GetComponent<MeshRenderer>().enabled = true;

        }
        else{
           
            map.GetComponent<MeshRenderer>().enabled = false;
            map2.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

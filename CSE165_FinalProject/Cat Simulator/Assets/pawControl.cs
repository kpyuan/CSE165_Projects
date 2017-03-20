using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawControl : MonoBehaviour {
    public OVRInput.Controller controller;
    public bool left;
    public bool right;
    public float positionOffset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (left)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch)+new Vector3(0,0,0);
            transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
        else if(right)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + new Vector3(0, 0, 0);
            transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        }
        
    }
}

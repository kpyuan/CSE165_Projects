using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetControl : MonoBehaviour {
    Camera cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        //this aint correct
        gameObject.transform.position = cam.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.transform.position.z));
        
    }
}

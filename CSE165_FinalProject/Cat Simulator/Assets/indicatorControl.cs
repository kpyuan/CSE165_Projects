using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorControl : MonoBehaviour {
    public Camera camera;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.Euler(0, -Mathf.Rad2Deg * camera.transform.localRotation.y+180, 0);
	}
}

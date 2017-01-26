using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reticleControl : MonoBehaviour {
    Image reticle;
    Camera cam;
	// Use this for initialization
	void Start () {
        reticle = gameObject.GetComponent<Image>();
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        GameObject target;
        float value=0;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)){ 
            target = hit.collider.gameObject;
            //if it's not in empty mode OR it's selecting the selector then you set value
            if (gaze.selection != 0 || target.tag == "selector"){
                value = gaze.dwellTime / 2;
            }
        }
        //animate the radial action with the set value
        reticle.fillAmount = value;
	}
}

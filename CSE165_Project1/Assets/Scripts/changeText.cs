using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour {
    Text mode;
	// Use this for initialization
	void Start () {
        mode = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //change text accordingly
		if (gaze.selection == 0){
            mode.text = "Mode: None";
        }else if (gaze.selection == 1){
            mode.text = "Mode: Canon Ball";
        }
        else if(gaze.selection==2){
            mode.text = "Mode: Laser";
        }
	}
}

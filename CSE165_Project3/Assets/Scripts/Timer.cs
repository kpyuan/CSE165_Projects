using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    private float timer;
    Text timeTxt;
	// Use this for initialization
	void Start () {
        timer = 0.0f;
        timeTxt = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timeTxt.text = timer.ToString()+"\n"+racetrack.DistanceLength.ToString()+" FT";
        if (!racetrack.finished && Countdown.startEnabled)
        {
            timer += Time.deltaTime;
        }
        
    }
}

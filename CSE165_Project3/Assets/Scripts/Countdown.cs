using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    Text countTxt;
    static float timeLeft;
    public static bool startEnabled;
    public static bool finished;
    // Use this for initialization
    void Start () {
        countTxt = gameObject.GetComponent<Text>();
        timeLeft = 5.0f;
        startEnabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        countTxt.text = Mathf.Round(timeLeft).ToString();
        if (timeLeft <= 0 && !startEnabled){
            sounds.playStart();
            countTxt.text = "";
            startEnabled = true;
        }
    }

    public static void reset() {
        sounds.playCrash();
        startEnabled = false;
        timeLeft = 3.0f;
    }
}

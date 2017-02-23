using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    Text countTxt;
    float timeLeft;
    public static bool startEnabled;
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
        if (timeLeft <= 0)
        {
            countTxt.text = "";
            startEnabled = true;

        }

    }
}

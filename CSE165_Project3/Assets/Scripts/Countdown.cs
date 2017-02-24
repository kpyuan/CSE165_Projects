using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    Text countTxt;
    static float timeLeft;
    public static bool startEnabled;
    private static bool cntdownFinished;

    //sound jazz
    public AudioClip crashSound;
    public AudioClip startSound;
    private static AudioClip crashSoundClone;
    // Use this for initialization
    void Start () {
        countTxt = gameObject.GetComponent<Text>();
        timeLeft = 5.0f;
        startEnabled = false;
        cntdownFinished = false;
        crashSoundClone = crashSound;
    }
	
	// Update is called once per frame
	void Update () {
        if (!cntdownFinished)
        {
            timeLeft -= Time.deltaTime;
            countTxt.text = Mathf.Round(timeLeft).ToString();
        }
        if (timeLeft < 0 )
        {
            sounds.playAudioEffect(startSound);
            countTxt.text = "";
            startEnabled = true;
            cntdownFinished = true;
            timeLeft = 0;
        }
        
        
    }

    public static void reset() {
        sounds.playAudioEffect(crashSoundClone);
        startEnabled = false;
        cntdownFinished = false;
        timeLeft = 3.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour {
    public static AudioSource startSound;
    public static AudioSource checkpointSound;
    public static AudioSource crashSound;
    public static AudioSource finishSound;

    void Start () {}
	void Update () {}

    public static void playStart(){
        startSound.Play();
    }
    public static void playCheckpoint(){
        checkpointSound.Play();
    }
    public static void playCrash(){
        crashSound.Play();
    }
    public static void playFinish(){
        finishSound.Play();
    }
}

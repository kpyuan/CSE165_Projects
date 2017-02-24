using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour {
    private static AudioSource player;

    void Start () {
        player = GetComponent<AudioSource>();
        
    }
	void Update () {}

    public static void playAudioEffect(AudioClip sound){
        player.clip = sound;
        player.Play();
    }
}

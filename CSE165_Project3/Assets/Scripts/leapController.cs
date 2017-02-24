using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class leapController : MonoBehaviour {
    LeapProvider provider;
    public GameObject player;


    void Start() {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void Update() {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands) {
            if (hand.IsRight) {
                if (Countdown.startEnabled && !racetrack.finished)
                {
                    Vector3 direction = new Vector3(hand.Direction.x, hand.Direction.y, hand.Direction.z);
                    Vector3 localDirection = player.transform.InverseTransformDirection(direction);
                    player.transform.Translate(localDirection * Time.deltaTime * 400.0f);
                }
                
            }
        }
    }
}

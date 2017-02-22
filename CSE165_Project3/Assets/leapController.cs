﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class leapController : MonoBehaviour {
    LeapProvider provider;
    public Camera cam;
    public GameObject player;

    void Start() {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void Update() {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands) {
            if (hand.IsRight) {
                Vector3 direction = new Vector3(hand.Direction.x, hand.Direction.y, hand.Direction.z);
                /*float angle = Vector3.Angle(direction, cam.transform.forward);
                Vector3 axis = Vector3.Cross(direction, cam.transform.forward);
                cam.transform.RotateAround(cam.transform.position, axis, angle*Time.deltaTime);*/
                player.transform.Translate(direction*Time.deltaTime*250.0f);
            }
        }
    }
}

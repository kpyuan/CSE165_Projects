using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class controlMap : MonoBehaviour
{
    LeapProvider provider;

    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void Update()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft)
            {
                Vector3 direction = new Vector3(hand.Direction.x, hand.Direction.y, hand.Direction.z);
                transform.position = hand.PalmPosition.ToVector3() -hand.PalmNormal.ToVector3() *(transform.localScale.y * .5f + .02f);
                transform.rotation = hand.Basis.CalculateRotation();
            }
        }
    }
}

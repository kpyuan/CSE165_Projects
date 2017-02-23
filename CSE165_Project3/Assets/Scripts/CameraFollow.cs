using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Target;
    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y+200.0f, Target.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gaze : MonoBehaviour {
    Camera cam;
    public Rigidbody cannonball;

    void Start() { 
        Debug.Log("loading gaze script");
        cam = Camera.main;
    }
    
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            FireCannon();
        }
    }

    void FireCannon() {
        //this code only fires when the player is looking directly at a physics object
        /*RaycastHit hit;
        Ray sight = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(sight, out hit, 100) || true) {
            Rigidbody cannonballClone = (Rigidbody) Instantiate(cannonball, cam.transform.position, Quaternion.identity);
            cannonballClone.velocity = sight.direction*25;
        }*/

        //this code always fires
        Rigidbody cannonballClone = (Rigidbody)Instantiate(cannonball, cam.transform.position, Quaternion.identity);
        cannonballClone.velocity = cam.transform.forward * 25;
    }
}

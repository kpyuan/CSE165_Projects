using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {
    public Rigidbody cannonball;
    Camera cam;

    void Start() { 
        Debug.Log("loading cannon script");
        cam = Camera.main;
    }
    
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Fire();
        }
    }

    void Fire() {
        RaycastHit hit;
        Ray sight = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(sight, out hit, 100)) {
            Rigidbody cannonballClone = (Rigidbody) Instantiate(cannonball, cam.transform.position, Quaternion.identity);
            cannonballClone.velocity = sight.direction*25;
        }
    }
}

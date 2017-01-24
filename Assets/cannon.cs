using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {
    public Rigidbody cannonball;
    Camera cam;
    public static RaycastHit hit;
    Ray ray;
    public float offset;

    void Start() { 
        Debug.Log("loading cannon script");
        cam = Camera.main;
    }
    void Update () {
        ray = cam.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));

        Physics.Raycast(ray, out hit);
        //this will be according to camera gaze to
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire() {
        yield return new WaitForSeconds(0.05f);
        Rigidbody cannonballClone = (Rigidbody) Instantiate(cannonball, new Vector3(0, 4, 0), Quaternion.identity);
        cannonballClone.velocity = new Vector3(hit.point.x ,hit.point.y+offset,hit.point.z);
    }
}

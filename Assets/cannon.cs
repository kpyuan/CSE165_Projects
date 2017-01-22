using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {
    public Rigidbody cannonball;
    Camera cam;
	void Start () {
        Debug.Log("loading cannon script");
        cam = Camera.main;
        StartCoroutine(Fire());
    }
	
	void Update () {
	}

    IEnumerator Fire() {
        yield return new WaitForSeconds(5);
        Rigidbody cannonballClone = (Rigidbody) Instantiate(cannonball, new Vector3(0, 4, 0), Quaternion.identity);
        cannonballClone.velocity = new Vector3(0, 5, 25);
    }
}

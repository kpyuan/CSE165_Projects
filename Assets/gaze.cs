using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gaze : MonoBehaviour {
    Camera cam;
    public Rigidbody cannonball;
    public float cannonFireOffset; //for perfect accuracy
    LineRenderer line;

    void Start() { 
        Debug.Log("loading gaze script");
        cam = Camera.main;
        line = GetComponent<LineRenderer>();
        line.enabled = false;//disable the laser in the beginning of the game

    }
    
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            FireCannon();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            FireLaser();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("BrickWall",LoadSceneMode.Single);
        }
    }

    void FireCannon() {
        line.enabled = false;
        //this code only fires when the player is looking directly at a physics object
        RaycastHit hit;
        Ray sight = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(sight, out hit, 100) || true) {
            Rigidbody cannonballClone = (Rigidbody) Instantiate(cannonball, cam.transform.position, Quaternion.identity);
            cannonballClone.velocity = new Vector3(sight.direction.x,sight.direction.y+cannonFireOffset,sight.direction.z)*25;
        }

        //this code always fires
        /*Rigidbody cannonballClone = (Rigidbody)Instantiate(cannonball, cam.transform.position, Quaternion.identity);
        cannonballClone.velocity = cam.transform.forward * 25;*/
    }

    void FireLaser()
    {
        
        line.enabled = true;
        RaycastHit hit;
        Ray sight = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(sight, out hit, 100))
        {
            if (hit.collider)
            {
                line.SetPosition(1, new Vector3(0,0,hit.distance));
                Destroy(hit.transform.gameObject);
            }
            
        }
        else
        {
            line.SetPosition(1, new Vector3(0, 0, 5000));
        }
    }
}

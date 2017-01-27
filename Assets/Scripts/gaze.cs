﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gaze : MonoBehaviour {
    Camera cam;
    public Rigidbody cannonball;

    public static float dwellTime;
    private GameObject currentTarget;
    private GameObject previousTarget;
    public static int selection;

    void Start() { 
        Debug.Log("loading gaze script");
        cam = Camera.main;
        dwellTime = 0;
        selection = 0;
        previousTarget = null;
        currentTarget = null;
    }
    
    void Update () {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) { //looking at something
            currentTarget = hit.collider.gameObject;
            if (currentTarget.tag == "selector" || currentTarget.tag == "destructible") { //looking at selector or destructible
                if (previousTarget && previousTarget.GetInstanceID() == currentTarget.GetInstanceID()) { //looking at same object
                    dwellTime = dwellTime + Time.deltaTime; //Debug.Log("same obj " + dwellTime);
                    if (dwellTime > 2) { //activate
                        if (currentTarget.tag == "selector") { //change selection
                            selection = (selection + 1) % 3;
                            Debug.Log("selected: " + selection);
                            dwellTime = 0;
                        }
                        else if (currentTarget.tag == "destructible") { //nothing, cannon, or laser
                            if (selection == 1) {
                                FireCannon();
                            }
                            else if (selection == 2) {
                                Debug.Log("sent input");
                                FireLaser(currentTarget);
                            }
                            dwellTime = 0;
                        }
                    }
                }
                else { //looking at different object
                    dwellTime = 0;
                    previousTarget = currentTarget;
                }
            } else { //looking at neither selector nor destructible
                previousTarget = null;
                dwellTime = 0;
            }
        } else { //looking at nothing
            previousTarget = null;
            dwellTime = 0;
        }
        //restart game if looking straight up at sky
        if (checkUpAngle(cam.transform.forward, 10.0f)){
            SceneManager.LoadScene("BrickWall", LoadSceneMode.Single);
        }

        //for debugging purpose
        if (Input.GetKeyDown(KeyCode.Z)) {
            selection = (selection + 1) % 3;
            Debug.Log("selected: "+selection);
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            FireCannon();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("BrickWall",LoadSceneMode.Single);
        }
    }

    void FireCannon() {
        Rigidbody cannonballClone = (Rigidbody)Instantiate(cannonball, cam.transform.position, Quaternion.identity);
        cannonballClone.velocity = cam.transform.forward * 25;
    }

    void FireLaser(GameObject laserTarget) {
        if (laserTarget.tag == "destructible") {
            Destroy(laserTarget);
        }
    }
    //check to see if the angle between the gaze vector and up vector are within certain range
    bool checkUpAngle(Vector3 gaze, float angleRange)
    {
        float angle = Vector3.Angle(Vector3.up, gaze);
        //Debug.Log("angle is " + angle);
        if (angle <= angleRange){
            
            return true;
        }
        else{
            return false;
        }
    }
}
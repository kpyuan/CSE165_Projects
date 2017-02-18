using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    public GameObject desk;
    public GameObject deskReverse;
    public GameObject locker;
    public GameObject chair;
    public GameObject cabinet;
    public GameObject TV;
    public GameObject whiteboard;

    public OVRInput.Controller controller;

    int objectToken;
    bool toggleReady;
    bool shootReady;
    private int velocityPower;

	void Start () {
        objectToken = 0;
        toggleReady = false;
        shootReady = false;
        velocityPower = 2;
        createDesks(3,6);
        createLockers(5);
    }
	
	void Update () {
        float c = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller).x;
        if (Mathf.Abs(c) > 0.9){
            c = c / Mathf.Abs(c);
            if (toggleReady){
                toggleReady = false;
                objectToken = (objectToken + (int)c + 6) % 6;
                Debug.Log("objToken: " + objectToken);
            }
        } else {
            toggleReady = true;
        }
        //if right hand trigger is pressed
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.9) {
            if (shootReady) {
                shootReady = false;
                if (objectToken == 0) {
                    //spawn desk
                    GameObject deskClone = Instantiate(desk, this.transform.position, Quaternion.identity);
                    deskClone.GetComponent<Rigidbody>().velocity = this.transform.forward * velocityPower;
                } else if (objectToken == 1) {
                    //spawn chair
                    GameObject chairClone = Instantiate(chair, this.transform.position, Quaternion.identity);
                    chairClone.GetComponent<Rigidbody>().velocity = this.transform.forward * velocityPower;
                } else if (objectToken == 2) {
                    //spawn locker
                    GameObject lockerClone = Instantiate(locker, this.transform.position, Quaternion.identity);
                    lockerClone.GetComponent<Rigidbody>().velocity = this.transform.forward * velocityPower;
                } else if (objectToken == 3) {
                    //spawn cabinets
                    GameObject cabinetClone = Instantiate(cabinet, this.transform.position, Quaternion.identity);
                    cabinetClone.GetComponent<Rigidbody>().velocity = this.transform.forward * velocityPower;
                } else if (objectToken == 4) {
                    //spawn TV
                    GameObject TVClone = Instantiate(TV, this.transform.position, Quaternion.identity);
                    TVClone.GetComponent<Rigidbody>().velocity = this.transform.forward * velocityPower;
                } else if (objectToken == 5) {
                    //spawn whiteboard
                    spawnWhiteboard();
                }
            }
        } else {
            shootReady = true;
        }
    }

    void spawnWhiteboard() {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit)) { //pointing at something
            GameObject selection = hit.collider.gameObject;
            if (selection.tag == "wall") { //pointing at floor
                GameObject whiteboardClone = Instantiate(whiteboard, hit.point, Quaternion.identity);
                float angularDistance = Vector3.Angle(hit.normal, new Vector3(0, 0, 1));
                whiteboardClone.transform.Rotate(Vector3.Cross(new Vector3(1, 0, 21), hit.normal), angularDistance, Space.World);
            }
        }
    }

    void createLockers(int num) {
        float lockerOffset =0.18f;
        for (int i = 0; i < num; i++) {
            Vector3 position = new Vector3(-2.25f, 0.1f, 2.85f+lockerOffset * i);
            Instantiate(locker, position, Quaternion.identity);
        }
    }

    void createDesks(int width, int length) {
        float Woffset = 1.0f;
        float Loffset = 1.5f;
        int index = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                Vector3 position = new Vector3(-4.0f+Loffset*j, -0.206f, -2.0f+Woffset*i);
                Vector3 oppPosition = new Vector3(-4.31f+Loffset*j , -0.206f, -2.0f+Woffset*i);
                Instantiate(desk, position, Quaternion.identity);
                Instantiate(deskReverse, oppPosition, Quaternion.identity);
                index++;
            }
        }
    }
}

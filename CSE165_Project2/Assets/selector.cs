using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selector : MonoBehaviour {
    public OVRInput.Controller controller;
    public OVRCameraRig camera;

    private bool toggleReady;

    //for ray cast
    private LineRenderer laser;
    private List<GameObject> selections;
    private bool selectionReady;

    //for grabbing
    private GameObject selection;
    private bool grabbing;
    private float grabRadius;
    public LayerMask grabMask;
    private int switchToken;

    void Start() {
        selections = new List<GameObject>();
        switchToken = 0;
        toggleReady = true;
        selectionReady = true;
        selection = null;
        grabRadius = 0.3f;

        laser = gameObject.AddComponent<LineRenderer>();
        laser.material = new Material(Shader.Find("Particles/Additive"));
        laser.startColor = Color.yellow;
        laser.endColor = Color.red;
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
        laser.numPositions = 2;
    }

    void Update () {
        //toggle selector with B button
        if (OVRInput.Get(OVRInput.Button.Two)) {
            if (toggleReady) {
                toggleReady = false;
                switchToken = (switchToken + 1) % 2;
                toggleLaser();
            }
        } else {
            toggleReady = true;
        }
        if (switchToken == 0) {
            RCSelect();
        } else if (switchToken == 1) {
            grabSelect();
        }
        
    }

    //toggle laser
    void toggleLaser() {
        gameObject.GetComponent<LineRenderer>().enabled = !gameObject.GetComponent<LineRenderer>().enabled;
    }

    //Ray Casting select method
    void RCSelect() {
        laser.SetPosition(0, this.transform.position);
        laser.SetPosition(1, this.transform.position + this.transform.forward * 10);

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.9) { //hand trigger clears selection
            deselect();
        }

        if (selections.Count > 0) {
            float c = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller).x;
            if (c != 0) {
                foreach (GameObject selection in selections) {
                    selection.transform.Rotate(0, -30.0f * c * Time.deltaTime, 0, Space.Self);
                }
            }
        }

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0.9) {
            if (selectionReady) {
                selectionReady = false;
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, this.transform.forward, out hit)) { //pointing at something
                    laser.SetPosition(1, hit.point);
                    GameObject target = hit.collider.gameObject;
                    if (target.tag == "object" || target.tag == "board") { //pointing at selectable
                        if (!OVRInput.Get(OVRInput.Button.One)) {
                            deselect();
                        }
                        if (selections.Count > 0 && target.tag != selections[0].tag) {
                            return;
                        }
                        select(target);
                        selections.Add(target);
                    }
                    else if (selections.Count > 0 && target.tag == "floor") { //pointing at floor w/ active selection
                        GameObject lastSelected = selections[selections.Count-1];
                        Vector3 delta = new Vector3(hit.point.x - lastSelected.transform.position.x, 0, hit.point.z - lastSelected.transform.position.z);
                        foreach (GameObject selection in selections) {
                            if (selection.tag == "object") {
                                selection.transform.Translate(delta, Space.World);
                            }
                        }
                    }
                    else if (selections.Count > 0 && target.tag == "wall") { //pointing at wall w/ objects selection
                        foreach (GameObject selection in selections) {
                            if (selection.tag == "board") { //act on all boards
                                float angularDistance = Vector3.Angle(hit.normal, new Vector3(0, 0, 1));
                                selection.transform.position = hit.point;
                                selection.transform.rotation = Quaternion.identity;
                                selection.transform.Rotate(Vector3.Cross(new Vector3(1, 0, 21), hit.normal), angularDistance, Space.World);
                            }
                        }
                    }
                }
            }
        } else {
            selectionReady = true;
        }
    }

    void select(GameObject obj) {
        obj.layer = 9;
        Component[] childTransforms = obj.GetComponentsInChildren(typeof(Transform));
        foreach (Transform t in childTransforms) {
            t.gameObject.layer = 9;
        }
    }

    void deselect() {
        foreach (GameObject obj in selections) {
            obj.layer = 8;
            Component[] childTransforms = obj.GetComponentsInChildren(typeof(Transform));
            foreach (Transform t in childTransforms) {
                t.gameObject.layer = 8;
            }
        }
        selections.Clear();
    }

    //Virtual Hand select method
    void grabSelect() {
        grabbing = true;
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);
        if (hits.Length > 0){

            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].distance < hits[closestHit].distance) {
                    closestHit = i;
                }
            }
            if (selection==null && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.9 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0.9) {
                selection = hits[closestHit].transform.gameObject;
                selection.GetComponent<Rigidbody>().isKinematic = true;
                selection.transform.position = transform.position;
                selection.transform.parent = transform;
            }
        }

        //the code for throwing objects
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) < 0.1 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) < 0.1) {
            grabbing = false;
            if (selection != null) {
                selection.transform.parent = null;
                selection.GetComponent<Rigidbody>().isKinematic = false;

                selection.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
                selection = null;

            }
        }
    }
}

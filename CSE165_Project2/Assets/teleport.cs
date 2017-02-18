using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour {
    public OVRInput.Controller controller;
    public OVRCameraRig camera;

    private LineRenderer laser;
    private bool ready;
    void Start () {
        ready = false;

        laser = gameObject.AddComponent<LineRenderer>();
        laser.material = new Material(Shader.Find("Particles/Additive"));
        laser.startColor = Color.green;
        laser.endColor = Color.blue;
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
        laser.numPositions = 2;
    }
	
	void Update () {
        laser.SetPosition(0, this.transform.position);
        laser.SetPosition(1, this.transform.position + this.transform.forward * 10);

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.9) {
            if (ready) {
                ready = false;
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, this.transform.forward, out hit)) { //pointing at something
                    GameObject selection = hit.collider.gameObject;
                    if (selection.tag == "floor") { //pointing at floor
                        //Debug.Log("teleport to "+hit.point.ToString());
                        Vector3 delta = new Vector3(hit.point.x - camera.transform.position.x, 0, hit.point.z - camera.transform.position.z);
                        camera.transform.Translate(delta);
                    }
                }
            }
        } else {
            ready = true;
        }
    }
}

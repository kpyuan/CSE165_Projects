using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapControl : MonoBehaviour {
    public OVRInput.Controller controller;
    bool mapEnabled;
    // Use this for initialization
    void Start () {
        mapEnabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One,controller))
        {
            print(mapEnabled);
            if (mapEnabled)
            {
                mapEnabled = false;
            }else
            {
                mapEnabled = true;
            }
        }
        if (mapEnabled)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }else
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class saveLoad : MonoBehaviour {
    public OVRInput.Controller controller;
    private GameObject[] recordObjects;
    private GameObject[] newObjects;
    private StreamWriter file;
    private bool readyLoad;
    private bool readySave;
    
    void Start() {
        readyLoad = true;
        readySave = true;

    }
    
    void Update() {
        if (OVRInput.Get(OVRInput.Button.Three)) { //pressing x button
            if (readyLoad) {
                readyLoad = false;
                LoadData();
            }
        } else {
            readyLoad = true;
        }
        if (OVRInput.Get(OVRInput.Button.Four)) { //pressing y button
            if (readySave) {
                readySave = false;
                RecordData();
            }
        } else {
            readySave = true;
        }
    }

    void RecordData() {
        Debug.Log("saving");
        file = new StreamWriter("data.txt", false);
        GameObject []regObjects = GameObject.FindGameObjectsWithTag("object");
        GameObject []boardObjects = GameObject.FindGameObjectsWithTag("board");
        recordObjects = new GameObject[regObjects.Length + boardObjects.Length];
        regObjects.CopyTo(recordObjects, 0);
        boardObjects.CopyTo(recordObjects, regObjects.Length);
        foreach (GameObject obj in recordObjects)
        {
            file.WriteLine(obj.transform.position.x + " " + obj.transform.position.y + " " + obj.transform.position.z
                + " " + obj.transform.eulerAngles.x + " " + obj.transform.eulerAngles.y + " " + obj.transform.eulerAngles.z);

        }
        file.Close();
    }

    void LoadData() {
        Debug.Log("loading");
        //TODO NEED TO FIGURE OUT HOW TO DESTROY THE NEWLY SPAWNED OBJECT
        /*
        GameObject[] regObjects = GameObject.FindGameObjectsWithTag("object");
        GameObject[] boardObjects = GameObject.FindGameObjectsWithTag("board");
        newObjects = new GameObject[regObjects.Length + boardObjects.Length];
        regObjects.CopyTo(newObjects, 0);
        boardObjects.CopyTo(newObjects, regObjects.Length);
        foreach (GameObject obj in newObjects)
        {
            if (!Array.contains(recordObjects,obj))
            {
                Destroy(obj);
            }
            
        }*/
        int index = 0;
        string[] lines = System.IO.File.ReadAllLines("data.txt");
        foreach(string line in lines) {
            string[] words = line.Split();
            recordObjects[index].transform.position = new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2]));
            recordObjects[index].transform.rotation = Quaternion.Euler(new Vector3(float.Parse(words[3]), float.Parse(words[4]), float.Parse(words[5])));
            index++;
        }
    }
}

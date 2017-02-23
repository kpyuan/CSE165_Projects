using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racetrack : MonoBehaviour {
    public GameObject checkpoint;
    public GameObject player;
    public Material traversedMaterial;

    List<GameObject> track;
    GameObject nextCheckpoint;
    int nextIndex;
    void Start () {
        track = new List<GameObject>();
        generateTrack("track.txt");
        //have player start at the starting point
        player.transform.position = track[0].transform.position;
        //have player facing the 2nd checking point initially
       player.transform.LookAt(track[1].transform.position, new Vector3(0, 1, 0));
	}

    void Update() {
        print(nextIndex);
        if (nextIndex >= track.Count)
        {
            //WINNNN
            Countdown.startEnabled = false;
        }
        if (track.Count > 0) {
            if (Vector3.Distance(player.transform.position, nextCheckpoint.transform.position) < 36) {
                Debug.Log("Hit next");
                hitCheckpoint();
            }
        }
        
    }

    void generateTrack(string fileName) {
        GameObject previousCheckpoint = null;
        string[] lines = System.IO.File.ReadAllLines(fileName);
        foreach(string line in lines) {
            string[] coordinates = line.Split();
            Vector3 coordinateVector = new Vector3(float.Parse(coordinates[0]), float.Parse(coordinates[1]), float.Parse(coordinates[2]));
            GameObject checkpointClone = Instantiate(checkpoint, coordinateVector*0.1f, Quaternion.identity);
            if (previousCheckpoint) { //add line from previous checkpoint to current checkpoint
                LineRenderer lineRenderer = previousCheckpoint.AddComponent<LineRenderer>() as LineRenderer;
                lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.blue;
                lineRenderer.startWidth = 25.0f;
                lineRenderer.endWidth = 5.0f;
                lineRenderer.numPositions = 2;
                lineRenderer.SetPosition(0, previousCheckpoint.transform.position);
                lineRenderer.SetPosition(1, checkpointClone.transform.position);
            }
            previousCheckpoint = checkpointClone;
            track.Add(checkpointClone);
        }
        Debug.Log("track length: " + track.Count);
        nextIndex = 0;
        nextCheckpoint = track[nextIndex];
        Behaviour halo = (Behaviour)nextCheckpoint.GetComponent("Halo");
        halo.enabled = true;
    }

    void hitCheckpoint() {
        Behaviour halo = (Behaviour)nextCheckpoint.GetComponent("Halo");
        halo.enabled = false;
        nextCheckpoint.GetComponent<Renderer>().material = traversedMaterial;
        nextIndex = nextIndex+1;
        if (nextIndex < track.Count) {
            nextCheckpoint = track[nextIndex];
            Behaviour nextHalo = (Behaviour)nextCheckpoint.GetComponent("Halo");
            nextHalo.enabled = true;
        }
    }
}
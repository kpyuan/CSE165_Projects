using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racetrack : MonoBehaviour {
    public GameObject checkpoint;

	void Start () {
        generateTrack("track.txt");
	}

    void Update() {}

    void generateTrack(string fileName) {
        string[] lines = System.IO.File.ReadAllLines(fileName);

        GameObject previousCheckpoint = null;
        foreach (string line in lines){
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
            //TODO somehow globally track all checkpoints
        }

        //add halo to final checkpoint
        Behaviour halo = (Behaviour)previousCheckpoint.GetComponent("Halo");
        halo.enabled = true;
    }
}
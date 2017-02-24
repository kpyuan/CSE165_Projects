using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racetrack : MonoBehaviour {
    public GameObject checkpoint;
    public GameObject player;
    public Material traversedMaterial;

    List<GameObject> track;
    GameObject nextCheckpoint;
    GameObject lastCheckpoint;
    int nextIndex;
    public static bool finished;
    public static float DistanceLength;

    //audio jazz
    public AudioClip checkpointSound;
    public AudioClip finishSound;
    void Start () {
        track = new List<GameObject>();
        generateTrack("track.txt");
        //have player start at the starting point
        player.transform.position = track[0].transform.position;
        //have player facing the 2nd checking point initially
        player.transform.LookAt(track[1].transform.position, new Vector3(0, 1, 0));
	}

    void Update() {
        if (nextIndex >= track.Count && finished == false) { //WINNNN
            sounds.playAudioEffect(finishSound);
            finished = true;
        }
        if (track.Count > 0 && Countdown.startEnabled) {
            //do distance calculation, in feet, 1unit=10in=0.83333 ft
            DistanceLength = Vector3.Distance(player.transform.position, nextCheckpoint.transform.position) * 0.833333f;
            if (DistanceLength < 30) {
                Debug.Log("Hit next");
                hitCheckpoint();
            }

            if(player.transform.position.y < 0.00f) {
                player.transform.position = lastCheckpoint.transform.position;
                Countdown.reset();
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
        lastCheckpoint = track[0];
        Behaviour halo = (Behaviour)nextCheckpoint.GetComponent("Halo");
        halo.enabled = true;
    }

    void hitCheckpoint() {
        lastCheckpoint = nextCheckpoint;
        Behaviour halo = (Behaviour)nextCheckpoint.GetComponent("Halo");
        halo.enabled = false;
        nextCheckpoint.GetComponent<Renderer>().material = traversedMaterial;
        nextIndex = nextIndex+1;
        if (nextIndex < track.Count) {
            nextCheckpoint = track[nextIndex];
            Behaviour nextHalo = (Behaviour)nextCheckpoint.GetComponent("Halo");
            nextHalo.enabled = true;
            sounds.playAudioEffect(checkpointSound);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "building")
        {
            player.transform.position = lastCheckpoint.transform.position;
            Countdown.reset();
        }
        
    }
}
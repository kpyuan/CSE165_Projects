using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racetrack : MonoBehaviour {
	void Start () {
        generateTrack("track.txt");
	}

    void Update() {}

    void generateTrack(string fileName) {
        string[] lines = System.IO.File.ReadAllLines(fileName);
        foreach (string line in lines){
            string[] coordinates = line.Split();
            Vector3 coordinate = new Vector3(float.Parse(coordinates[0]), float.Parse(coordinates[1]), float.Parse(coordinates[2]));
            //create checkpoint gameobject
            //somehow globally track all checkpoints
        }
    }
}
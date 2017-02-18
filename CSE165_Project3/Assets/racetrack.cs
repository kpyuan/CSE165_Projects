using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadCheckpoints : MonoBehaviour {
	void Start () {
        string[] lines = System.IO.File.ReadAllLines("track.txt");
        foreach (string line in lines){
            string[] coordinates = line.Split();
        }
	}
	
	void Update () {
		
	}
}
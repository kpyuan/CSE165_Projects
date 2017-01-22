using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    public GameObject brick;
	void Start () {
        Debug.Log("Hello World");
        layLayer(24, 8.1f, 0.5f, 0.5f, 10);
    }
	
	void Update () {
	}

    void layCircle(int count, float radius, float height, float offset) {
        for(int i = 0; i < count; i++) {
            float angle = (i+offset) * Mathf.PI * 2 / count;
            Vector3 position = new Vector3(Mathf.Cos(angle)*radius, height, Mathf.Sin(angle)*radius);
            GameObject brickClone = (GameObject) Instantiate(brick, position, Quaternion.LookRotation(position-(new Vector3(0, height, 0))));
        }
    }

    void layLayer(int count, float radius, float height, float offset, float layers) {
        for (int i = 0; i < layers; i++) {
            layCircle(count, radius, height+(1.0f*i), offset*i);
        }
    }
}

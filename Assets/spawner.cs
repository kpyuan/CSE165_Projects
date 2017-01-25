using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    public GameObject brick;
	void Start () {
        Debug.Log("loading spawner script");
        //StartCoroutine(layLayer(24, 8.1f, 0.5f, 0.5f, 10));
        StartCoroutine(layLayer(48, 15.8f, 0.5f, 0.5f, 14));
    }
	
	void Update () {
	}

    IEnumerator layCircle(int count, float radius, float height, float offset) {
        for(int i = 0; i < count; i++) {
            yield return new WaitForSeconds(0.05f);
            float angle = (i+offset) * Mathf.PI * 2 / count;
            Vector3 position = new Vector3(Mathf.Cos(angle)*radius, height, Mathf.Sin(angle)*radius);
            /*GameObject brickClone = (GameObject) */Instantiate(brick, position, Quaternion.LookRotation(position-(new Vector3(0, height, 0))));
        }
    }

    IEnumerator layLayer(int count, float radius, float height, float offset, float layers) {
        for (int i = 0; i < layers; i++) {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(layCircle(count, radius, height+(1.0f*i), offset*i));
        }
    }
}

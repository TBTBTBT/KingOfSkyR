using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;
	int time = 0;
	int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time++;
		if (time > 320 - count) {
			if (count < 120)
				count++;
			Instantiate (enemy, new Vector3 (Random.Range (-10, 10), 10, 0), Quaternion.identity);
			time = 0;
			
		}
	}
}

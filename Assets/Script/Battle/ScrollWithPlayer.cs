using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWithPlayer : MonoBehaviour {
	Vector3 start;
	public float scale = 0.5f;
	public float speed = 1;
	public float limit = 1;
	Vector3 startPos;
	GameObject p;
	// Use this for initialization
	void Start () {
		start = transform.position;
		p = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (p) {
			if (Mathf.Abs(p.transform.position.x * scale) >= limit && p.transform.position.x * scale% limit <  ) {
				transform.localPosition = new Vector3 (limit, -p.transform.position.y * scale, transform.localPosition.z);
						}
			else
			transform.localPosition = new Vector3 (- (p.transform.position.x * scale) % limit, -p.transform.position.y*scale, transform.localPosition.z);
		}
		/*
			transform.position = new Vector3 (transform.position.x, start.y -p.transform.position.y * scale, transform.position.z);
		if (isSideScroll)
			transform.position += new Vector3 (speed, 0,0);
		else transform.position += new Vector3(0,speed,0);
		if (speed > 0) {
			if (transform.position.x > limit) {
				if (isSideScroll)
					transform.position -= new Vector3 ((limit - startPos.x), 0, 0);
				else
					transform.position -= new Vector3 (0, (limit - startPos.x), 0);
			}
		} else {
			if (transform.position.x < limit) {
				if (isSideScroll)
					transform.position -= new Vector3 ((limit - startPos.x), 0, 0);
				else
					transform.position -= new Vector3 (0, (limit - startPos.x), 0);
			}
		}*/
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightEnemy : FlightObjectBase {
	void TouchEnd(int num){
		if (num == 0) {
			isTouch = false;
		}
	}
	void TouchMove(int num){
		isTouch = true;
		if (bulletTime > bulletSpan) {
			for (int i = 0; i < 3; i++) {
				GameObject go = GameObject.Instantiate (Bullet, transform.position + new Vector3(loopX*2*(i-1),0,0), Quaternion.identity);
				go.GetComponent<BulletBase> ().Set (angle + anglespeed);

			}
			bulletTime = 0;
		}
	}
	protected override void ExtendFixedUpdate(){
		if (transform.position.y < 2)
			TouchMove (0);
		else
			TouchEnd (0);
	}
}

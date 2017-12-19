using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class FlightObjectBase : UtilityBehaviour{
	protected float angle = 0;
	protected float anglespeed = 0;
	protected float angleacc = 0.1f;
	protected float angledownacc = 0.1f;
	protected float acc = 5f;
	protected bool isTurn = false;
	protected bool isTouch = false;
	protected float angleX = 0;
	protected float maxHeight = 20;

	protected int body = 0;
	protected int wing = 0;
	protected float maxAcc = 5f;
	protected float minAcc = 5f;
	protected int hp = 3;

	public GameObject looksBasePrefab;
	public GameObject HitEffect;
	public GameObject DestroyEffect;
	//public GameObject looksRotateXPrefab;

	protected List<GameObject> looksBase = new List<GameObject>();
	protected List<GameObject> looksRotateX= new List<GameObject>();
	public GameObject Bullet;
	public float bulletSpan=0;
	public float bulletTime = 0;
	protected Rigidbody2D rigidbody;
	protected Vector2 velocity;

	protected float loopX = 80f;
	int destroyTime = 0;
	// Use this for initialization

	protected override void Init(){
		
		rigidbody = GetComponent<Rigidbody2D> ();
		for (int i = 0; i < 3; i++) {
			GameObject lb = GameObject.Instantiate (looksBasePrefab,transform);
			GameObject lrx1 = GameObject.Instantiate ((GameObject)Resources.Load("Parts/Body" + body),lb.transform);
			GameObject lrx2 = GameObject.Instantiate ((GameObject)Resources.Load("Parts/Wing" + wing),lrx1.transform);
			lrx2.transform.localScale = new Vector3 (1,1,1);
			lb.transform.position += new Vector3 (loopX*2 * (i - 1), 0, 0);
			looksBase.Add (lb);
			looksRotateX.Add (lrx1);

		}

	}
	protected virtual void ExtendFixedUpdate(){
	}
	protected void AutoPilot(){
		anglespeed = 0;
		angle = Mathf.Lerp (angle, 0, Time.deltaTime * 3);
	}
	void AccAdd(){
		float sin = Mathf.Sin (angle * Mathf.PI / 180);
		if (sin< 0) {
			if (acc < maxAcc)
				acc -= sin/15;
			if(acc >= maxAcc) acc = maxAcc;
		} else {
			if (acc > minAcc)
				acc -= sin/30;
			if(acc <= minAcc) acc = minAcc;
		}
	}
	// Update is called once per frame
	protected override void FixedUpdateCanPause() { 
		AccAdd ();
		if (hp > 0) {
			if (bulletTime <= bulletSpan)
				bulletTime++;
			if (!isTouch) {
				if (Mathf.Cos (angle * Mathf.PI / 180) > 0) {
					isTurn = false;
				} else {
					isTurn = true;

				}
			}
			if (isTouch) {
				anglespeed += angleacc;
			}else {
				Gravity (-angledownacc);
			}
			if (isTurn == false) {
				if (isTouch)
					angleX = Mathf.Lerp (angleX, 0, Time.deltaTime * 3);
				else {
					angleX = Mathf.Lerp (angleX, (Mathf.Cos (angle * Mathf.PI / 180)-1)*90, Time.deltaTime * 3);
				}
			} else {
				if (isTouch)
					angleX = Mathf.Lerp (angleX, -180, Time.deltaTime * 3);
				else {
					angleX = Mathf.Lerp (angleX, (Mathf.Cos (angle * Mathf.PI / 180)-1)*90, Time.deltaTime * 3);
				}
			}

			Move ();


			if (isTurn == false) {
				angle += anglespeed;
			} else {
				angle -= anglespeed;
			}

			anglespeed *= 0.9f;
			rigidbody.velocity = velocity;
			for (int i = 0; i < 3; i++) {
				looksBase [i].transform.localRotation = Quaternion.AngleAxis (angle, new Vector3 (0, 0, 1));
				looksRotateX [i].transform.localRotation = Quaternion.AngleAxis (angleX, new Vector3 (1, 0, 0));
			}
			ExtendFixedUpdate ();
			if (transform.position.x < -loopX) {
				transform.position += new Vector3 (loopX * 2, 0, 0);
			}
			if (transform.position.x > loopX) {
				transform.position -= new Vector3 (loopX * 2, 0, 0);
			}
			//takasaseigen
			if (rigidbody.velocity.y > 0) {
				float ex = (maxHeight - transform.position.y) > 3 ? 1 : (maxHeight - transform.position.y)/3;
				rigidbody.velocity = new Vector2 (rigidbody.velocity.x, rigidbody.velocity.y * ex);
				Gravity ((1 - ex) * -angledownacc);
			}
		} else {
			rigidbody.velocity = Vector2.zero;
			destroyTime ++;
			if (destroyTime > 100) {
				Destroy (gameObject);
			}
		}
	}
	/*
	public float GetAim(Vector2 p1, Vector2 p2) {
		float dx = p2.x - p1.x;
		float dy = p2.y - p1.y;
		float rad = Mathf.Atan2(dy, dx);
		return rad * Mathf.Rad2Deg;
	}*/
	void Move(){
		velocity = Forward (angle);

	}
	void Gravity(float g){

		anglespeed += g;

		//rigidbody.velocity += new Vector2 (0, g);
	//	anglespeed -= 0.01f;
	}
	Vector2 Forward(float angle){
		return new Vector2(Mathf.Cos(angle * Mathf.PI / 180),Mathf.Sin(angle * Mathf.PI / 180)) * acc;
	}
	public virtual void Damage(int d){
		hp -= d;
		for (int i = 0; i < 3; i++) {
			if (HitEffect) {
				GameObject go = GameObject.Instantiate (HitEffect,transform);
				go.transform.localPosition = new Vector3 (loopX * 2 * (i - 1), 0, 0);
			}
		}
		if (hp <= 0) {
			for (int i = 0; i < 3; i++) {
				if (DestroyEffect) {
					GameObject go = GameObject.Instantiate (DestroyEffect,transform);
					go.transform.localPosition = new Vector3 (loopX * 2 * (i - 1), 0, 0);
				}
				Destroy (looksRotateX[i].gameObject);


			}
			GetComponent<Collider2D> ().enabled = false;

		}
	}
}

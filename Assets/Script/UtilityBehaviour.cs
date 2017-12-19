using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityBehaviour : MonoBehaviour {
	void Start () {
		EventManager.OnSpawnDialog.AddListener (Stop);
		EventManager.OnDisappearDialog.AddListener (Go);
		Init ();
	}
	protected virtual void Init(){
	}
	public void Pause(){
	}
	protected bool isStop = false;
	void Update(){
		if (!isStop)
			UpdateCanPause ();
	}
	void FixedUpdate(){
		if (!isStop)
			FixedUpdateCanPause ();
	}
	protected virtual void UpdateCanPause(){
		
	}
	protected virtual void FixedUpdateCanPause(){
	}
	public float GetAim(Vector2 p1, Vector2 p2)
	{
		float dx = p2.x - p1.x;
		float dy = p2.y - p1.y;
		float rad = Mathf.Atan2(dy, dx);
		return rad * Mathf.Rad2Deg;
	}
	Vector2? velbuffer;
	public void Stop(){
		isStop = true;
		if(velbuffer == null)velbuffer = GetComponent<Rigidbody2D> ().velocity;
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero  ;
	}
	public void Go(){
		isStop = false;
		if(velbuffer != null)GetComponent<Rigidbody2D> ().velocity = (Vector2)velbuffer;
		velbuffer = null;
	}
}

public class TickEvent{
	public delegate void callBack ();
	List<callBack> c = new List<callBack>();
	int max = 0;
	int time = 0;
	public TickEvent(int tick){
		max = tick;
	}
	public void SetSpan(int t){
		max = t;
	}
	public void SetFunction(callBack cb){
		c.Add (cb);
	}
	public void Reset (){
		time = 0;
	}
	public void Invoke(){
		time++;
		if (time >= max) {
			time = 0;
			foreach (callBack cb in c) {
				cb ();
			}
		}
	}
}
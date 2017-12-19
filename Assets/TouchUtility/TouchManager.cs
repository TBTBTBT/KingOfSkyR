using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TouchManager : SingletonMonoBehaviour<TouchManager> {
	//Dictionary<int,TouchInfo> touchState = new Dictionary<int,TouchInfo>();
	/*public TouchInfo GetTouch(int i){
		if (touchState.ContainsKey (i)) {
			return touchState [i];
		}
		return TouchInfo.None;
	}*/
	public Vector3 GetTouchWorldPos(int num){
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		return TouchInput.GetTouchWorldPosition (cam, num);
	}
	void Update () {
		
		int touchNum = 1;
		#if (UNITY_EDITOR || UNITY_STANDALONE)
		#else

		touchNum = Input.touchCount;

		#endif
		for(int i = 0;i < touchNum; i++){
			if (TouchInput.GetTouch(i) == TouchInfo.Began)EventManager.Invoke  (ref EventManager.OnTouchBegin,TouchInput.GetID(i));
			if (TouchInput.GetTouch(i) == TouchInfo.Moved || TouchInput.GetTouch(i) == TouchInfo.Stationary)EventManager.Invoke (ref EventManager.OnTouchMove,TouchInput.GetID(i));
			if (TouchInput.GetTouch(i) == TouchInfo.Ended)EventManager.Invoke  (ref EventManager.OnTouchEnd,TouchInput.GetID(i));
		}/*
		for(int i = 0;i < touchNum; i++){
				if (touchState.ContainsKey (i)) {
				touchState [i] = TouchInput.GetTouch (i);
				//	Debug.Log (i);
				} else {
	
				touchState.Add (i, TouchInfo.Began);
				}
		}*/
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.OnTouchBegin.AddListener (Touch);
	}
	
	void Touch(int i){
		FaderBehaviour.Instance.Fade ("MissionSelect");
	}
}

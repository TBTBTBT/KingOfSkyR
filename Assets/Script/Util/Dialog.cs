using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {
	public Animator dialog;
	bool isAppear = false;
	public bool isChangeScene =false;
	public string sceneName = "";
	// Use this for initialization
	void Start () {
	}
	public void PressOk(){
		dialog.SetTrigger ("ok");
		if (isChangeScene) {
			FaderBehaviour.Instance.Fade (sceneName);
		}
	}
	// Update is called once per frame
	void Update () {
		if (!isAppear) {
			isAppear = true;
			EventManager.Invoke(ref EventManager.OnSpawnDialog);
		}
		if (dialog.GetCurrentAnimatorStateInfo (0).IsName ("End")) {
			EventManager.Invoke(ref EventManager.OnDisappearDialog);
			Destroy (this.gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class SelectMissionManager : MonoBehaviour {
	public GameObject UI;
	public GameObject AreaButton;
	public GameObject areaButtonPrefab;
	public GameObject MissionButton;
	public GameObject missionButtonPrefab;
	bool isMoveUp = false;
	int areaId = 0;
	int missionId = 0;

	// Use this for initialization
	void Start () {
		SpawnAreaButton (areaButtonPrefab, new Vector2 (-200, 0), ()=>{MoveUpUI(0);});
		SpawnMissionButton (missionButtonPrefab, new Vector2 (-200, 0), ()=>{GoMission(0);});
	}
	void SpawnAreaButton(GameObject b,Vector2 pos,UnityAction d){
		GameObject ins = GameObject.Instantiate(b,AreaButton.transform);
		ins.transform.localPosition = pos;
		ins.GetComponent<Button> ().onClick.AddListener (d);
	}
	void SpawnMissionButton(GameObject b,Vector2 pos,UnityAction d){
		GameObject ins = GameObject.Instantiate(b,MissionButton.transform);
		ins.transform.localPosition = pos;
		ins.GetComponent<Button> ().onClick.AddListener (d);
	}
	public void MoveUpUI(int aid){
		isMoveUp = true;
		areaId = aid;
	}
	public void GoMission(int mid){
		missionId = mid;
		FaderBehaviour.Instance.Fade ("Battle");
	}
	// Update is called once per frame
	void Update () {
		if (isMoveUp) {
			UI.transform.localPosition = Vector3.Lerp (UI.transform.localPosition, new Vector3 (0, 500, 0), Time.deltaTime * 5);
		}
		else {
			UI.transform.localPosition = Vector3.Lerp (UI.transform.localPosition, new Vector3 (0, 0, 0), Time.deltaTime * 5);

		}
	}
}

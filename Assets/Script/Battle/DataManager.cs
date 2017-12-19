using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager> {
	int missionid = 0;
	public List<dataMission> missionData = new List<dataMission>();
	public int selectMission = 0;

	// Use this for initialization
	void Start () {

		LoadMissionData ();
	}

	void LoadMissionData(){
		//dataMission m = new dataMission ();
		for (int i = 0; i < 1; i++) {
			dataMission m = new dataMission (){
				enemyMaxNum = 2,
				id = 0,
				type = MissionType.DestroyAll
			};
			m.enemies = new List<DataEnemy> ();
			for (int j = 0; j < 1; j++) {
				m.enemies.Add (new DataEnemy (){ num = 0, hp = 0 });
			}
			missionData.Add (m);
		}

	}

}

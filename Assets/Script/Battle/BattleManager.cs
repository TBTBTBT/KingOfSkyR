using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : MonoBehaviour {
	List<GameObject> enemy;
	int maxEnemy = 0;
	string prefabPath = "Enemy/enemy";
	public GameObject spawnPos;
	public GameObject UIInfo;
	public GameObject UIClear;
	public Canvas canvas;
	dataMission currentMission;
	int enemyCount = 0;
	bool isClear = false;
	bool isEnd = false;
	bool isStop=false;
	// Use this for initialization
	void Start () {
		currentMission = DataManager.Instance.missionData [DataManager.Instance.selectMission];
		EventManager.OnSpawnDialog.AddListener (Stop);
		EventManager.OnDisappearDialog.AddListener (Go);
		enemy = new List<GameObject> ();
		//mission = new Mission();
		//dummy
		//maxEnemy = mission.enemyMaxNum;
		//ダイアログ表示など
		Instantiate (UIInfo, canvas.transform);
	}
	void SpawnEnemy(){
		if (currentMission.enemies.Count > enemyCount) {
			GameObject go = GameObject.Instantiate ((GameObject)Resources.Load (prefabPath + currentMission.enemies[enemyCount].num), spawnPos.transform.position, Quaternion.identity);
			//enemyの設定 enemiesに入っている
			enemy.Add (go);
			enemyCount++;
		}
	}
	public void Stop(){
		isStop = true;
	}
	public void Go(){
		isStop = false;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isEnd == false && isStop == false) {
			for (int i = 0; i < enemy.Count; i++) {
				if (enemy [i] == null)
					enemy.RemoveAt (i);
			}
			if (currentMission.enemyMaxNum > enemy.Count) {
				SpawnEnemy ();
				//	Instantiate ((GameObject)Resources.Load(prefabPath + "0"), spawnPos.transform.position, Quaternion.identity);
			}
			if (currentMission.type == MissionType.DestroyAll) {
				//Debug.Log (currentMission.enemies.Count + "," + enemyCount + "," + enemy.Count);
				if (currentMission.enemies.Count <= enemyCount && enemy.Count == 0) {
					isClear = true;
					isEnd = true;
					Instantiate (UIClear, canvas.transform);
				}
			}
		}
	}
}

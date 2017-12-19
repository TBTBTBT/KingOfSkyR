using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MissionType{
	DestroyAll,
	DestroyBoss,
}
public struct DataEnemy{
	public int num;
	public int hp ;

}
public class dataMission {
	public int id;
	public MissionType type;
	public int enemyMaxNum = 1;
	public List<DataEnemy> enemies;


}

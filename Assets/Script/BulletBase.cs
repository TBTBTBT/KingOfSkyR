using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BulletBase : UtilityBehaviour {

    protected Rigidbody2D rigidbody;
    [Header("横スクロール")]
    bool isSideScroll = true;
    [Header("プレイヤーの弾")]
    public bool isPlayer = false;

    [Header("弾が消える時間")]
    public int MaxLifeTime = 100;
    [Header("スピード")]
    public float speed = 10;
    [Header("加速度")]
    public float accel = 0;
    //[Header("さらに、弾をセット")]
    //public GameObject bullet;
	[Header("ヒットエフェクト")]
	public GameObject hit;
    protected float direction;
    protected float lifeTime;
	// Use this for initialization
	void Awake () {
	//	Debug.Log(transform.position);

        rigidbody = GetComponent<Rigidbody2D>();
		direction = 0;

	}
	public void Set(float d){
		direction = d;
		transform.localRotation = Quaternion.AngleAxis(direction , new Vector3(0, 0, 1));
	}
	/*
    void AimPlayer(Vector2 pos){
        if (player)
        {
			//Debug.Log(pos);
            direction = GetAim(pos, player.transform.position);
            transform.localRotation = Quaternion.AngleAxis(direction - 90, new Vector3(0, 0, 1));
		//	Debug.Log(direction);
        }
    }*/
	// Update is called once per frame
    protected override void FixedUpdateCanPause()
    {
		transform.localRotation = Quaternion.AngleAxis(direction , new Vector3(0, 0, 1));
		speed += accel;
		rigidbody.velocity = new Vector2(Mathf.Cos(direction*Mathf.PI/180),Mathf.Sin(direction*Mathf.PI/180))* speed;
        lifeTime++;
        if (lifeTime > MaxLifeTime)
        {
			Hit ();
        }
		UpdateLate ();
    }
	protected virtual void UpdateLate(){
	}
    protected virtual void OnHitEnemy(Collider2D g)
    {
		g.GetComponent<FlightObjectBase> ().Damage (1);
		Destroy (this.gameObject);
    }
	protected virtual void OnHitPlayer(Collider2D g)
    {
		Destroy (this.gameObject);
    }
	protected void Hit(){
		if(hit)
		Instantiate (hit, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
    void OnTriggerEnter2D(Collider2D c)
    {
        if (isPlayer)
        {
			if (c.transform.tag == "Enemy")
            {
                OnHitEnemy(c);
            }
            
        }
        else
        {
			if (c.transform.tag == "Player")
            {
                OnHitPlayer(c);
            }
        }
    }
}

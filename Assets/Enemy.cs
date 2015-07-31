using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform player;
	public float speed;
	public float distance;
	public bool chase;
	public float timeStamp;
	public float coolDown;
	public float bulletSpeed;
	public GameObject bulletFlash;
	public GameObject flashPoint;
	public Rigidbody2D bullet;
	public float pause;
	public float health = 10;
	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		if (Time.time>= pause && Time.time >= timeStamp && chase) {
			Shoot();
		}
		if (player != null && Vector3.Distance (transform.position, player.position) < distance) {
			chase = true;
		} else {
			chase=false;
		}
		if(health<=0){
			Destroy(gameObject);
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		if(player!=null && chase &&Vector3.Distance(transform.position,player.position)>0.5){
			float direction = Mathf.Atan2((player.position.y-transform.position.y),(player.transform.position.x-transform.position.x)) * Mathf.Rad2Deg - 90;
			transform.eulerAngles = new Vector3(0,0,direction);
			rigidbody2D.AddForce(gameObject.transform.up*speed*Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name.Equals ("laserRed(Clone)")) {

			health-=collider.transform.GetComponent<Bullet>().damage;
		}
	}

	void Shoot(){
		timeStamp = Time.time + coolDown;
		GameObject flash = Instantiate(bulletFlash,flashPoint.transform.position,transform.rotation) as GameObject;
		Destroy (flash,0.05f);
		Rigidbody2D bulletShot=Instantiate(bullet,transform.position,transform.rotation) as Rigidbody2D;
		bulletShot.rigidbody2D.AddForce(transform.up*bulletSpeed);
	}

}

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
	public float rotationSpeed;
	public bool dead;
	public Sprite destroyed;
	public GameObject explosion;
	public GameObject gameController;
	// Use this for initialization
	void Start () {
		Invoke ("ChangeDirection", rotationSpeed);
		dead = false;
	}

	void Update(){
		if (Time.time>= pause && Time.time >= timeStamp && chase && !dead) {
			Shoot();
		}
		if (player != null && Vector3.Distance (transform.position, player.position) < distance && !dead) {
			chase = true;
		} else {
			Move();
			chase=false;
		}
		if(health<=0){

			if(destroyed!=null && !dead){
				Explode();
				gameController.GetComponent<GameController>().enemies-=1;
				gameObject.GetComponent<SpriteRenderer>().sprite=destroyed;
				dead=true;
			}else if (!dead){
				Explode();
				gameController.GetComponent<GameController>().enemies-=1;
				dead=true;
				Destroy(gameObject);
			}
			dead=true;
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		if(player!=null && chase &&Vector3.Distance(transform.position,player.position)>0.9 && transform.name=="enemy" && !dead){
			float direction = Mathf.Atan2((player.position.y-transform.position.y),(player.transform.position.x-transform.position.x)) * Mathf.Rad2Deg - 90;
			transform.eulerAngles = new Vector3(0,0,direction);
			rigidbody2D.AddForce(gameObject.transform.up*speed*Time.deltaTime);
		}
		else if(player!=null && chase &&Vector3.Distance(transform.position,player.position)>0.9 && transform.name=="Turret_Gun" && !dead){
			float direction = Mathf.Atan2((player.position.y-transform.position.y),(player.transform.position.x-transform.position.x)) * Mathf.Rad2Deg;
			transform.eulerAngles = new Vector3(0,0,direction);
			rigidbody2D.AddForce(gameObject.transform.up*speed*Time.deltaTime);
		}

	}
	void ChangeDirection(){
		if (Random.value > 0.5f) {
			rotationSpeed = -rotationSpeed;
		}
		Invoke ("ChangeDirection", rotationSpeed);
	}
	void Move(){
		if (!dead) {
			transform.Rotate (new Vector3 (0, 0, rotationSpeed * Time.deltaTime));
			rigidbody2D.AddForce (gameObject.transform.up * speed * Time.deltaTime);
		}
	}

	void Explode(){
		explosion.GetComponent<Animator> ().speed = 10f;
		GameObject explode = Instantiate(explosion,transform.position,transform.rotation) as GameObject;
		Destroy (explode,1.0f);
	}

	void OnCollisionEnter2D(Collision2D collider){
		if(collider.gameObject.name=="planet"){
			transform.Rotate(new Vector3(0,0,100));
		}
		else if(collider.gameObject.name=="Background"){
			transform.Rotate(new Vector3(0,0,160));
		}
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name.Equals ("laserRed(Clone)") && !dead) {
			health-=collider.transform.GetComponent<Bullet>().damage;
			Destroy(collider.gameObject);
		}
	}

	void Shoot(){
		timeStamp = Time.time + coolDown;
		GameObject flash = Instantiate(bulletFlash,flashPoint.transform.position,transform.rotation) as GameObject;
		Destroy (flash,0.05f);
		Rigidbody2D bulletShot=Instantiate(bullet,flashPoint.transform.position,flashPoint.transform.rotation) as Rigidbody2D;
		if(transform.name=="Turret_Gun"){
			bulletShot.rigidbody2D.AddForce(transform.right*bulletSpeed);
		}
		else if(transform.name=="enemy")
		{
			bulletShot.rigidbody2D.AddForce(transform.up*bulletSpeed);
		}
	}

}

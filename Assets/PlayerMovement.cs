using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed;
	public float rotationSpeed;
	public Rigidbody2D bullet;
	public float bulletSpeed;
	public GameObject bulletFlash;
	public GameObject smokeTrail;
	public GameObject flashPoint;
	public GameObject trailPoint;
	public float coolDown;
	public float timeStamp;
	public float health=100;
	public bool poweredUp;
	public GameObject explosion;
	public float count;
	// Use this for initialization
	void Start () {
		poweredUp = false;
		count = 0.0f;
	}
	void Update(){
		if (poweredUp) {
			Debug.Log (count);
			count++;
			if(count>80){
				poweredUp=false;
				count=0;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (Vector3.forward * rotationSpeed, rotationSpeed);
			GenerateTrail();
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (Vector3.back * rotationSpeed, rotationSpeed);
			GenerateTrail();
		}
		if (Input.GetKey (KeyCode.W)) {
			rigidbody2D.AddForce (transform.up * speed);
			GenerateTrail();
		}
		if (Input.GetKey (KeyCode.S)) {
			rigidbody2D.AddForce (transform.up * -speed);//Because moving backwards is slower than forwards	
			GenerateTrail();
		}
		if(Input.GetKey (KeyCode.Space)){

			if(!poweredUp&&timeStamp <= Time.time){
				Shoot();
			}
			else if(poweredUp){
				Shoot ();
			}
		}
		if (health <= 0) {
			Explode();
			Destroy(gameObject);
		}
	}

	void Explode(){
		explosion.GetComponent<Animator> ().speed = 10f;
		GameObject explode = Instantiate(explosion,transform.position,transform.rotation) as GameObject;
		Destroy (explode,1.0f);
		GameOver ();
	}

	void GameOver(){

	}

	void GenerateTrail(){
		GameObject smoke = Instantiate(smokeTrail,trailPoint.transform.position,transform.rotation) as GameObject;
		Destroy (smoke, 0.5f);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.name.Equals ("laserGreen(Clone)")){
			Destroy(collider.gameObject);
			health-=collider.transform.GetComponent<Bullet>().damage;
		}
		if(collider.name.Equals("powerup")){
			Destroy(collider.gameObject);
			poweredUp=true;
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

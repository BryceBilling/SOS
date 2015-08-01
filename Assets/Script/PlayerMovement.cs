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
	public GameObject gameOver;
	public GameObject shield;
	public AudioClip explosionSound;
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
				var shield = transform.Find("shield");
				shield.GetComponent<SpriteRenderer>().enabled=false;
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
			Application.LoadLevel ("GameOver");
		}

	}

	void Explode(){
		transform.audio.PlayOneShot (explosionSound);
		explosion.GetComponent<Animator> ().speed = 10f;
		GameObject explode = Instantiate(explosion,transform.position,transform.rotation) as GameObject;
		Destroy (explode,1.0f);
	}
	

	void GenerateTrail(){
		GameObject smoke = Instantiate(smokeTrail,trailPoint.transform.position,transform.rotation) as GameObject;
		if (health > 60 && !poweredUp) {
			smoke.GetComponent<SpriteRenderer> ().color = Color.green;
		}
		else if (health > 30 && health <= 60 && !poweredUp) {
			smoke.GetComponent<SpriteRenderer> ().color = Color.yellow;
		}
		else if (health > 0 && health <= 30 && !poweredUp) {
			smoke.GetComponent<SpriteRenderer> ().color = Color.red;
		}
		else if(poweredUp){
			smoke.GetComponent<SpriteRenderer> ().color = Color.blue;
		}
		Destroy (smoke, 0.5f);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.name.Equals ("laserGreen(Clone)") && !poweredUp){
			Destroy(collider.gameObject);
			health-=collider.transform.GetComponent<Bullet>().damage;
		}
		if(collider.name.Equals("powerup")){
			var shield = transform.Find("shield");
			shield.GetComponent<SpriteRenderer>().enabled=true;
			Destroy(collider.gameObject);
			poweredUp=true;
		}
		if(collider.name.Equals("asteroid(Clone)")){
			health-=collider.transform.GetComponent<Asteroid>().damage;
			Destroy(collider.gameObject);
		}
	}

	void Shoot(){
		gameObject.audio.Play ();
		timeStamp = Time.time + coolDown;
		GameObject flash = Instantiate(bulletFlash,flashPoint.transform.position,transform.rotation) as GameObject;
		Destroy (flash,0.05f);
		Rigidbody2D bulletShot=Instantiate(bullet,transform.position,transform.rotation) as Rigidbody2D;
		bulletShot.rigidbody2D.AddForce(transform.up*bulletSpeed);
	}
}

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
	// Use this for initialization
	void Start () {
	
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
			rigidbody2D.AddForce (transform.up * -speed / 2);//Because moving backwards is slower than forwards	
			GenerateTrail();
		}
		if(Input.GetKey (KeyCode.Space)){

			if(timeStamp <= Time.time){
				Shoot();
			}
		}
		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	void GenerateTrail(){
		GameObject smoke = Instantiate(smokeTrail,trailPoint.transform.position,transform.rotation) as GameObject;
		Destroy (smoke, 0.5f);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log (collider.name);
		if(collider.name.Equals ("laserGreen(Clone)")){
			Destroy(collider.gameObject);
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

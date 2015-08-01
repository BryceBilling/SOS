using UnityEngine;
using System.Collections;

public class CreateAsteroid : MonoBehaviour {
	public float time;
	public Rigidbody2D asteroid;
	public float speed;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Make", 4,time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Make(){
		Rigidbody2D ast = Instantiate (asteroid, transform.position, transform.rotation) as Rigidbody2D;
		ast.rigidbody2D.AddForce(transform.up*speed);
	}
}

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float damage=20;
	// Use this for initialization
	void Start () {
		Invoke ("Remove", 0.6f);
	}
	
	// Update is called once per frame
	void Remove () {
		Destroy (gameObject);
	}	


}

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float damage=10;
	// Use this for initialization
	void Start () {
		Invoke ("Remove", 1.5f);
	}
	
	// Update is called once per frame
	void Remove () {
		Destroy (gameObject);
	}	


}

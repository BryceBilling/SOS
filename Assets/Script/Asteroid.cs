using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public float damage=50;
	// Use this for initialization
	void Start () {
		Invoke ("Remove", 5f);
	}
	
	// Update is called once per frame
	void Remove () {
		Destroy (gameObject);
	}
}

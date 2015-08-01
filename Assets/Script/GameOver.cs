using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Reload", 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Reload(){
		Application.LoadLevel (0);
	}
}

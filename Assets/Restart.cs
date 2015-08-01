using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	public GameObject boss;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (boss == null) {
			Invoke ("Reload", 2.0f);
		}
	}

	void Reload(){
		Application.LoadLevel (0);
	}
}

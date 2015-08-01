using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform player;
	public float mapYSize;
	public float mapXSize;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			Vector3 position = new Vector3 (player.position.x, player.position.y, transform.position.z);
			position.x = Mathf.Clamp (position.x, -mapXSize, mapXSize);
			position.y = Mathf.Clamp (position.y, -mapYSize, mapYSize);
			transform.position = position;
		}
	}
}

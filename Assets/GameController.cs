using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int enemies;
	public GameObject player;
	public GameObject prepare;
	public bool GameOver;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(enemies==0 && prepare!=null && !GameOver){
			GameObject prepareText = Instantiate(prepare,player.transform.position,transform.rotation) as GameObject;
			Destroy(prepareText,2f);
			Invoke ("NextLevel",2.0f);
			GameOver=true;
		}
	}

	void NextLevel(){
		Application.LoadLevel("Boss");
	}
}

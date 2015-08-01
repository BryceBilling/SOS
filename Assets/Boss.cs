using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	public float health= 200;
	public GameObject explosion;
	public GameObject win;
	public GameObject player;
	public GameObject bossTxt;
	public AudioClip explosionSound;
	// Use this for initialization
	void Start () {
		GameObject bossText = Instantiate(bossTxt,player.transform.position,transform.rotation) as GameObject;
		Destroy(bossText,1f);
	}
	
	// Update is called once per frame
	void Update () {

		if(health<=0){
			Debug.Log("Dead");
			Explode();
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name.Equals ("laserRed(Clone)")) {

			health-=collider.transform.GetComponent<Bullet>().damage;
		}
	}

	void Explode(){
		transform.audio.PlayOneShot (explosionSound);
		GameObject explo=Instantiate(explosion,transform.position,transform.rotation ) as GameObject;
		explo.transform.localScale=new Vector3(100,100,100);
		Destroy(explo,2f);
		GameObject winText = Instantiate(win,player.transform.position,transform.rotation ) as GameObject;
	}
}

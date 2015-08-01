using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {
    public GameObject boss;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (boss == null)
        {
            Invoke("Reload",3f);

        }
	}

    void Reload()
    {
        Application.LoadLevel(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUnlocking : MonoBehaviour {
    public GameObject key;
    public GameObject door;
	public GameObject player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider coll)
    {
		Debug.Log("collision!");
		if (coll.CompareTag("Player"))
        {
            //transform.localScale *= 0;
            door.transform.localScale *= 0;
            key.SetActive(false);
        }
    }
}

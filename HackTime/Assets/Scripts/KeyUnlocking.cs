using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUnlocking : MonoBehaviour {
    public GameObject door;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        transform.localScale *= 0;
        door.transform.localScale *= 0;
    }
}

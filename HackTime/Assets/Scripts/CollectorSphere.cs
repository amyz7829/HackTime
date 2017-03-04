using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorSphere : MonoBehaviour {
	public bool hasBeenCollected;
	// Use this for initialization
	void Start () {
		hasBeenCollected = false;
	}
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			hasBeenCollected = true;
			gameObject.SetActive (false);
		}
	}
}

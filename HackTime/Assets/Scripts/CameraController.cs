using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		transform.position = player.transform.position; 
		transform.rotation = player.transform.rotation; 
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position; 
		transform.rotation = player.transform.rotation;
	}
}

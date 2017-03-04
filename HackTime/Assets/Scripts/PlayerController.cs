using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float moveVertical = Input.GetAxis ("Vertical") * speed;
		float turnAngle = Input.GetAxis ("Horizontal") * rotationSpeed;
		moveVertical *= Time.deltaTime; 
		turnAngle *= Time.deltaTime;
		transform.Translate (0, 0, moveVertical);
		transform.Rotate (0, turnAngle, 0);
	}
}

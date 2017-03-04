using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
	public float vz, z;

	// Use this for initialization
	void Start () {
		vz = 1;
		z = 0;
        Debug.Log("Hi!");
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = GetComponent<Rigidbody> ();
        Debug.Log(rb);
        Debug.Log(rb.position);
        Vector3 currentPos = rb.position;
        rb.MovePosition(currentPos + (0.1f * Vector3.left * vz));

		float oldVZ = vz;
		vz -= 0.15f * z;
		z += 0.15f * oldVZ;
        //Renderer r = GetComponent<Renderer>();
        //r.material.color = Color.blue;
	}
}

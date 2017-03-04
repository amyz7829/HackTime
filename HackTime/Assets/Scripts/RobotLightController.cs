using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLightController : MonoBehaviour {
	public GameObject robot;
	private TimeObject timeObj;
	public Light light; 

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		timeObj = robot.GetComponent<TimeObject>();
		transform.position = robot.transform.position; 
		transform.rotation = robot.transform.rotation; 
	}

	// Update is called once per frame
	void LateUpdate () {
		if (!timeObj.visible) {
			light.enabled = false;
		} else {
			light.enabled = true;
		}
		transform.position = robot.transform.position; 
		transform.rotation = robot.transform.rotation;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLightController : MonoBehaviour {
	public GameObject robot;
	private TimeObject timeObj;
	private Light light; 

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		timeObj = robot.GetComponentInParent<TimeObject>();
	}

	//If the robot is currently visible, then the light is on. Otherwise it's not
	void LateUpdate () {
		if (!timeObj.visible) {
			light.enabled = false;
		} else {
			light.enabled = true;
		}
	}
}

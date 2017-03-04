using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour {
	public bool is_future, visible;
	public float last_angle;
	public GameObject player; 

	// Use this for initialization
	void Start () {
		visible = true;
		if (is_future) {
			if ((getRelativeAngle() + 180) % 360 > 180) {
				transform.localScale *= 0;
				visible = false;
			}
		} else {
			if ((getRelativeAngle() + 180) % 360 <= 180) {
				transform.localScale *= 0; 
				visible = false;
			}
		}
		last_angle = getRelativeAngle();
	}
	
	// Update is called once per frame
	void Update () {
		float new_angle = (getRelativeAngle() + 360) % 360;
		bool crossed_behind = (90 < new_angle && new_angle < 180 && 270 > last_angle && last_angle >= 180)
			|| (90 < last_angle && last_angle < 180 && 270 > new_angle && new_angle >= 180);
		visible ^= crossed_behind;
		if (!visible) {
			transform.localScale *= 0;
		}
		else{
			transform.localScale = new Vector3(1, 1, 1);
		}
		last_angle = new_angle;
	}

	float getRelativeAngle(){
		Vector3 positionVector = player.transform.position - transform.position;
		float result = ((player.transform.eulerAngles.y - Mathf.Rad2Deg * Mathf.Atan2 (positionVector.z, positionVector.x)) + 180) % 360;
		return result;
	}
}

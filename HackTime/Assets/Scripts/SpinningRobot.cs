using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRobot : MonoBehaviour {
	public float rotation_speed;
	private Vector3 rotation;
	public GameObject player;
	public float distance_to_catch;

	// Use this for initialization
	void Start () {
		rotation = new Vector3 (0, rotation_speed * Time.deltaTime, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotation);

		if (can_see(player)) {
			Debug.Log ("Game Over!");
		}
	}

	bool can_see(GameObject o){
		float angle_between = Vector3.Angle ((player.transform.position - transform.position), transform.rotation.eulerAngles);
		if (angle_between > 85) {
			return false;
		} 
		else {
			RaycastHit hit;
			Vector3 playerDirection = Vector3.Normalize((transform.position - player.transform.position));
			Physics.Raycast (transform.position, playerDirection, out hit, distance_to_catch);

			if (hit.transform.gameObject.tag == "Player") {
				return true;
			} 
			else {
				return false;
			}
		}
	}
}

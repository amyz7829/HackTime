using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingRobot : MonoBehaviour {
	public Vector3 start_position, end_position;
	public float speed;
	Vector3 forwards_direction;
	Vector3 current_direction;
	public float distance_travelled;
	public float distance;
	public float distance_tick; 
	public GameObject player;
	private TimeObject timeObj;
	public float distance_to_catch;

	// Use this for initialization
	void Start () {
		transform.position = start_position;
		forwards_direction = Vector3.Normalize (end_position - start_position);
		current_direction = forwards_direction;
		transform.rotation = Quaternion.Euler (forwards_direction.x, forwards_direction.y, forwards_direction.z);

		distance = Vector3.Distance (start_position, end_position);
		distance_travelled = 0;
		speed = speed;

		timeObj = GetComponent<TimeObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 previous_position = transform.position;
		if (distance_travelled <= distance) {
			transform.position += speed * Time.deltaTime * forwards_direction;
			distance_travelled += Vector3.Distance (previous_position, transform.position);
		} 
		else {
			speed = -1 * speed;
			if (forwards_direction == current_direction) {
				current_direction = Vector3.Normalize (start_position - end_position);
			} else {
				current_direction = forwards_direction;
			}
			distance_travelled = 0;
			transform.RotateAround (transform.position, Vector3.up, 180);
		}

		if (can_see(player) && timeObj.visible) {
			Debug.Log ("Game Over!");
		}
	}

	bool can_see(GameObject player){
		if (Vector3.Angle ((player.transform.position - transform.position), current_direction) < 85) {
			RaycastHit hit;
			Vector3 playerDirection = Vector3.Normalize (player.transform.position - transform.position);

			if (Physics.Raycast (transform.position, playerDirection, out hit, distance_to_catch)) {
				print (hit.transform.gameObject.tag);
				if (hit.transform.gameObject.tag == "Player") {
					return true;
				} else {
					return false;
				}
			}
			return false;
		} else {
			return false;
		}
	}
}

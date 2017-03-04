using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrollingRobot : MonoBehaviour {
	//The two positions the robot patrols between
	public Vector3 start_position, end_position;

	//Speed of robot
	public float speed;

	public float rotationSpeed;

	//Direction of start facing end
	Vector3 forwards_direction;

	//Direction of end facing start 
	Vector3 backwards_direction;

	//The current direction
	Vector3 current_direction;

	//How much distance has been travelled in the current cycle
	public float distance_travelled;

	//Distance between the two points
	public float distance;

	//The player object, used to calculate whether robot has seen player
	public GameObject player;

	//Time object, used to tell whether robot is visible
	private TimeObject timeObj;

	//How close the robot must be to "catch" the player
	public float distance_to_catch;

	private bool gameOver;
	private Vector3 killerLocation;
	private Vector3 killerDirection;
	private Quaternion lookRotation;

	// Use this for initialization
	void Start () {
		transform.position = start_position;
		forwards_direction = Vector3.Normalize (end_position - start_position);
		backwards_direction = Vector3.Normalize (start_position - end_position);
		current_direction = forwards_direction;
        transform.rotation = Quaternion.LookRotation(current_direction);

		distance = Vector3.Distance (start_position, end_position);
		distance_travelled = 0;

		timeObj = GetComponent<TimeObject> ();
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			Vector3 previous_position = transform.position;
			if (distance_travelled <= distance) {
				transform.position += speed * Time.deltaTime * current_direction;
				distance_travelled += Vector3.Distance (previous_position, transform.position);
			} else {
				if (forwards_direction == current_direction) {
					current_direction = backwards_direction;
				} else {
					current_direction = forwards_direction;
				}
				distance_travelled = 0;
				transform.rotation = Quaternion.LookRotation (current_direction);
			}

			if (can_see (player) && timeObj.visible) {
				Debug.Log ("Game Over!");
				gameOver = true;
				killerLocation = transform.position;
			}
		} else {
			killerDirection = Vector3.Normalize (killerLocation - player.transform.position);
			lookRotation = Quaternion.LookRotation (killerDirection);

			transform.rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
			if (Input.anyKey) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
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

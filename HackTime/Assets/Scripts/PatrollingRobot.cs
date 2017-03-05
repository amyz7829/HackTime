using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrollingRobot : MonoBehaviour {
	// The two positions the robot patrols between
	public Vector3 start_position, end_position;

	// Speed of robot
	public float speed;

	float rotationSpeed = 1;

	//Direction of start facing end
	Vector3 forwards_direction;

	// Direction of end facing start 
	Vector3 backwards_direction;

	// The current direction
	Vector3 current_direction;

	// How much distance has been travelled in the current cycle
	float distance_travelled = 0;

	// Distance between the two points
	float distance;

	// The player object, used to calculate whether robot has seen player
	public GameObject player;

	public GameObject gameOver;

	// Time object, used to tell whether robot is visible
	private TimeObject timeObj;

	// How close the robot must be to "catch" the player
	public float distance_to_catch;

	bool isGameOver = false;
	PlayerControllerMouse controller;

    // Sets a limit to how far to the left / right the guard can 'see'.
    // If negative, then there is no limit.
    public float horizontal_distance_to_catch;

	// Use this for initialization
	void Start () {
		transform.position = start_position;
		forwards_direction = Vector3.Normalize(end_position - start_position);
		backwards_direction = Vector3.Normalize(start_position - end_position);
		current_direction = forwards_direction;
        transform.rotation = Quaternion.LookRotation(current_direction);

		distance = Vector3.Distance (start_position, end_position);

		timeObj = GetComponentInParent<TimeObject> ();

		controller = player.GetComponent<PlayerControllerMouse> ();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 previous_position = transform.position;
        if (distance_travelled <= distance)
        {
            transform.position += speed * Time.deltaTime * current_direction;
            distance_travelled += Vector3.Distance(previous_position, transform.position);
        }
        else
        {
            if (forwards_direction == current_direction)
            {
                current_direction = backwards_direction;
            }
            else
            {
                current_direction = forwards_direction;
            }
            distance_travelled = 0;
            transform.rotation = Quaternion.LookRotation(current_direction);
        }
        if (!isGameOver)
		{
			Vector3 playerDirection = Vector3.Normalize (player.transform.position - transform.position);
			Debug.DrawRay (transform.position, playerDirection);
            if (can_see(player))
            {
                Debug.Log("Game Over!");
                isGameOver = true;
                gameOver.SetActive(true);
                controller.controlEnabled = false;
            }
        }
        else
        {
            Vector3 killerDirection = Vector3.Normalize(transform.position - player.transform.position);
            //killerDirection = Vector3.Normalize (killerLocation - player.transform.position);
            // Track the killer's position with the camera
            Quaternion lookRotation = Quaternion.LookRotation(killerDirection);

            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                gameOver.SetActive(false);
                controller.controlEnabled = true;
            }
			if (Input.GetKey (KeyCode.Backspace)) {
				gameOver.SetActive (false);
				controller.controlEnabled = true;
				isGameOver = false;
			}
        }
		
	}

	bool can_see(GameObject player){
        if (transform.localScale.magnitude == 0)
        {
            return false;
        }

		if (Vector3.Angle ((player.transform.position - transform.position), current_direction) < 85) {
			RaycastHit hit;
			Vector3 playerDirection = Vector3.Normalize (player.transform.position - transform.position);

			if (Physics.Raycast (transform.position, playerDirection, out hit, distance_to_catch) &&
                (horizontal_distance_to_catch < 0 || 
                Vector3.Cross(player.transform.position - transform.position, current_direction).magnitude <= horizontal_distance_to_catch)) {
				// print (hit.transform.gameObject.name);
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

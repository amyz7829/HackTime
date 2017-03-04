using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinningRobot : MonoBehaviour {
	public float rotation_speed;
	private Vector3 rotation;
	public GameObject player;
	public float distance_to_catch;
	Vector3 current_direction;

	// Use this for initialization
	void Start () {
		rotation = new Vector3 (0, rotation_speed * Time.deltaTime, 0);
		current_direction = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotation);
		current_direction += rotation;
		if (can_see(player)) {
			Debug.Log ("Game Over!");
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	bool can_see(GameObject o){
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

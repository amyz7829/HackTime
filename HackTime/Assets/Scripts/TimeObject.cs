using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour {
    enum Direction { Left, Right, None };
	//This boolean says if something is in the future or in the present
	public bool is_future;

	//Determines whether the object is currently visible
	public bool visible;

	public float last_angle;
	public GameObject player;

    private Vector3 original_scale;
    private float side_threshold = 120;

    // Objects will only switch visibility if they pass out of view and enter into view from
    // different directions.
    private Direction last_threshold_passed = Direction.None;

	// Objects in the present start out visible.
	void Start () {
        visible = !is_future;
        original_scale = transform.localScale;
        last_angle = getRelativeAngle();
    }
	
	// Calculations are made to determine whether to switch visibility
	void Update () {
		float new_angle = getRelativeAngle();
        // Passed into field of view from the left
        if (last_angle <= -side_threshold && -side_threshold < new_angle && new_angle < 0) {
            visible ^= (last_threshold_passed == Direction.Right);
            last_threshold_passed = Direction.None;
        }
        // Passed into field of view from the right
        if (last_angle >= side_threshold && 0 < new_angle && new_angle < side_threshold) {
            visible ^= (last_threshold_passed == Direction.Left);
            last_threshold_passed = Direction.None;
        }
        // Passed out of field of view to the left
        if (-side_threshold <= last_angle && last_angle < 0 && new_angle < -side_threshold) {
            last_threshold_passed = Direction.Left;
        }
        // Passed out of field of view to the right
        if (0 < last_angle && last_angle <= side_threshold && side_threshold < new_angle) {
            last_threshold_passed = Direction.Right;
        }
        if (!visible) {
            transform.localScale *= 0;
        }
        else {
            transform.localScale = original_scale;
        }
        last_angle = new_angle;
	}

	float getRelativeAngle(){
        Vector3 positionVector = transform.position - player.transform.position;
        return ((player.transform.eulerAngles.y + Mathf.Rad2Deg * Mathf.Atan2(positionVector.z, positionVector.x)) + 90 + 360) % 360 - 180;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObjectPair : MonoBehaviour
{
    enum Direction { Left, Right, None };
    //This boolean says if something is in the future or in the present
    private bool is_future;

    private float last_angle;
    public GameObject player;

    public GameObject current_self;
    public GameObject future_self;

    private Vector3 self_original_scale;
    private Vector3 future_self_original_scale;

    private float side_threshold = 120;

    // Objects will only switch visibility if they pass out of view and enter into view from
    // different directions.
    private Direction last_threshold_passed = Direction.None;

    // Objects in the present start out visible.
    void Start()
    {
        is_future = false;

        if (current_self == null)
        {
            current_self = new GameObject();
            current_self.transform.position = future_self.transform.position;
        }
        else if (future_self == null)
        {
            future_self = new GameObject();
            future_self.transform.position = current_self.transform.position;
        }

        self_original_scale = current_self.transform.localScale;
        future_self_original_scale = future_self.transform.localScale;

        last_angle = getRelativeAngle(current_self);
    }

    // Calculations are made to determine whether to switch visibility
    void Update()
    {
        float new_angle;
        if (is_future)
        {
            new_angle = getRelativeAngle(future_self);
        }
        else
        {
            new_angle = getRelativeAngle(current_self);
        }

        // Passed into field of view from the left
        if (last_angle <= -side_threshold && -side_threshold < new_angle && new_angle < 0)
        {
            is_future ^= (last_threshold_passed == Direction.Right);
            last_threshold_passed = Direction.None;
        }
        // Passed into field of view from the right
        if (last_angle >= side_threshold && 0 < new_angle && new_angle < side_threshold)
        {
            is_future ^= (last_threshold_passed == Direction.Left);
            last_threshold_passed = Direction.None;
        }
        // Passed out of field of view to the left
        if (-side_threshold <= last_angle && last_angle < 0 && new_angle < -side_threshold)
        {
            last_threshold_passed = Direction.Left;
        }
        // Passed out of field of view to the right
        if (0 < last_angle && last_angle <= side_threshold && side_threshold < new_angle)
        {
            last_threshold_passed = Direction.Right;
        }

        if (is_future)
        {
            if (future_self.activeSelf)
            {
                future_self.transform.localScale = future_self_original_scale;
                current_self.transform.localScale *= 0;
            }
        }
        else
        {
            if (current_self.activeSelf)
            {
                current_self.transform.localScale = self_original_scale;
                future_self.transform.localScale *= 0;
            }
        }

        last_angle = new_angle;
    }

    float getRelativeAngle(GameObject currentlyVisible)
    {
        Vector3 positionVector = currentlyVisible.transform.position - player.transform.position;
        return ((player.transform.eulerAngles.y + Mathf.Rad2Deg * Mathf.Atan2(positionVector.z, positionVector.x)) + 90 + 360) % 360 - 180;
    }
}

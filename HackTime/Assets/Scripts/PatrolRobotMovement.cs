using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRobotMovement : MonoBehaviour
{
    public float secondsForOneLength;
    public float rotationTime;
    public Vector3 pos1;
    public Vector3 pos2;

    // Use this for initialization
    IEnumerator Start()
    {
        pos1 = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pos1, pos2, secondsForOneLength));
            yield return StartCoroutine(RotateRight(180));
            yield return StartCoroutine(MoveObject(transform, pos2, pos1, secondsForOneLength));
            yield return StartCoroutine(RotateRight(180));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2,
             Mathf.SmoothStep(0f, 1f,
              Mathf.PingPong(Time.time / secondsForOneLength, 1f)
            ));
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    IEnumerator RotateRight(float angle)
    {
        float normalizationFactor = 1.0f / rotationTime; //We need to normalize time since slerp() works with values between 0-1; we can convert values by multiplying with this factor
        float timePassed = 0.0f; //Time passed since the start of the linear interpolation. Starting at 0, it increases until it reaches 1. All values are rendered.

        while (timePassed < 1.0f) //While the time passes is less than 1 (the maximum of a linear interpolation)
        {
            timePassed += Time.deltaTime * normalizationFactor; //Increase the timePassed with the time passed since the last frame; the time is first normalized
            transform.RotateAround(transform.position, transform.up, timePassed * angle);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
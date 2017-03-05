using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the player object
public class PlayerControllerKey : MonoBehaviour {
    public float speedV = 4.0F, speedH = 4.0F;
    public float rotationSpeed = 150F;
    public float tiltSpeed = 0.05f;
    public float tiltRange = 50f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    private float pitch = 0;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float verticalMove = (Input.GetKey("w") ? 1 : Input.GetKey("s") ? -1 : 0);
        float horizMove = (Input.GetKey("d") ? 1 : Input.GetKey("a") ? -1 : 0);
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(horizMove * speedH, 0, verticalMove * speedV);
            moveDirection = transform.TransformDirection(moveDirection);
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        
        float turnAngle = -(Input.GetKey("left") ? 1 : Input.GetKey("right") ? -1 : 0) * rotationSpeed;
        turnAngle *= Time.deltaTime;
        controller.transform.Rotate(0, turnAngle, 0, Space.World);

        float oldPitch = pitch;
        float tiltView = -(Input.GetKey("up") ? 1 : Input.GetKey("down") ? -1 : 0);
        float tiltDecay = 1 - tiltSpeed;
        pitch *= tiltDecay;
        pitch += tiltRange * tiltView * (1 - tiltDecay);
        controller.transform.Rotate(pitch - oldPitch, 0, 0, Space.Self);
    }
}

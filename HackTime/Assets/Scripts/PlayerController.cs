using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the player object
public class PlayerController : MonoBehaviour {
    public float speed = 4.0F;
    public float rotationSpeed = 150F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public bool controlEnabled = true;

    private Vector3 moveDirection = Vector3.zero;
    private float pitch = 0;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float verticalMove = controlEnabled? (Input.GetKey("up") ? 1 : Input.GetKey("down") ? -1 : 0):0;
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, verticalMove);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (controlEnabled && Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        
        float turnAngle = controlEnabled? (Input.GetAxis("Horizontal") * rotationSpeed):0;
        turnAngle *= Time.deltaTime;
        controller.transform.Rotate(0, turnAngle, 0, Space.World);

        float oldPitch = pitch;
        float tiltView = (Input.GetKey("s") ? 1 : Input.GetKey("w") ? -1 : 0);
        float tiltDecay = 0.85f;
        pitch *= tiltDecay;
        pitch += 50 * tiltView * (1 - tiltDecay);
        controller.transform.Rotate(pitch - oldPitch, 0, 0, Space.Self);
    }
}

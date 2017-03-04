using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the player object
public class PlayerController : MonoBehaviour {
    public float speed = 4.0F;
    public float rotationSpeed = 150F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        float turnAngle = Input.GetAxis("Horizontal") * rotationSpeed;
        turnAngle *= Time.deltaTime;
        controller.transform.Rotate(0, turnAngle, 0);
    }
}

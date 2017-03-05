using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the player object
public class PlayerControllerMouse : MonoBehaviour {
    public float speedV = 4.0F, speedH = 4.0F;
    public float rotationSpeed = 250F;
    public float tiltSpeed = 2f;
    public float tiltRange = 50f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    private float pitch = 0;

    private bool mouseLocked = true;
    public bool controlEnabled = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (controlEnabled)
        {
            CharacterController controller = GetComponent<CharacterController>();
            float verticalMove = Input.GetAxis("Vertical");
            float horizMove = Input.GetAxis("Horizontal");
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(horizMove * speedH, 0, verticalMove * speedV);
                moveDirection = transform.TransformDirection(moveDirection);
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;

            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

            if (mouseLocked)
            {
                float turnAngle = Input.GetAxis("Mouse X") * rotationSpeed;
                turnAngle *= Time.deltaTime;
                controller.transform.Rotate(0, turnAngle, 0, Space.World);

                float oldPitch = pitch;
                float tiltView = -Input.GetAxis("Mouse Y") * tiltSpeed;
                pitch += tiltView;
                pitch = Mathf.Clamp(pitch, -tiltRange, tiltRange);
                controller.transform.Rotate(pitch - oldPitch, 0, 0, Space.Self);
            }
        }
    }
    void OnGUI()
    {
        // Toggle cursor on escape keypress
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLocked = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseLocked = true;
        }
    }
}

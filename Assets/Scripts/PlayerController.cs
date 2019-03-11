using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpAmount;
    [SerializeField] private float gravity;

    Vector3 movement;
    Vector3 moveDirection;

    float inputX;
    float inputZ;

    CharacterController characterController;
    Camera cam;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    private void Update()
    {
        RotateToFaceCameraForward();

        Movement();
    }

    private void Movement()
    {
        if (characterController.isGrounded)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            Vector3 movementInput = new Vector3(inputX, 0, inputZ);
            movement = transform.TransformDirection(movementInput);

            if (Input.GetButton("Jump"))
            {
                movement.y = jumpAmount;
            }
        }

        movement.y = movement.y - (gravity * Time.deltaTime); // Apply gravity each frame.

        characterController.Move(movement * moveSpeed * Time.deltaTime); // Move player using character controller
    }

    private void RotateToFaceCameraForward()
    {
        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0f;
        moveDirection = cameraForward.normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotateSpeed);
    }
}

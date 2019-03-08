using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpAmount;
    [SerializeField] private float gravity;

    Vector3 movement;
    bool canDoubleJump;
    int doubleJumpAmount = 2;
    int doubleJumpCount;

    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (characterController.isGrounded)
        {

            Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement = transform.TransformDirection(movementInput);
            movement *= moveSpeed;

            if (Input.GetButton("Jump"))
            {
                movement.y = jumpAmount;
            }
        }

        movement.y = movement.y - (gravity * Time.deltaTime);

        characterController.Move(movement * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground") && !characterController.isGrounded)
        {
            Debug.Log("Hit ground");
            canDoubleJump = true;
        }
    }
}

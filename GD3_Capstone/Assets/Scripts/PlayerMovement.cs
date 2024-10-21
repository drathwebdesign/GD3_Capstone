using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Movement Variables")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 7f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float gravityMultiplier = 2f;

    [Header("Grounded")]
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] bool isGrounded = false;
    [SerializeField] LayerMask ground;

    Vector3 velocity;
    private float currentSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        currentSpeed = walkSpeed;
        //multiply gravity by multiplier
        gravity *= gravityMultiplier;
    }

    void Update()
    {
        //movement
        GroundCheck();
        ApplyGravity();
        OnJump();
        OnSprint();
        OnMove();
    }

    private void OnMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        characterController.Move(move.normalized * currentSpeed * Time.deltaTime);
       
        characterController.Move(velocity * Time.deltaTime);
    }
    private void OnSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = sprintSpeed;
        else
            currentSpeed = walkSpeed;
    }
    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    private void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2.0f;
        else
            velocity.y += gravity * Time.deltaTime;
    }
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, characterController.height / 2 + groundDistance, ground))
            isGrounded = true;
        else
            isGrounded = false;
    }
}

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Movement Variables")]
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float gravityMultiplier = 2f;

    [Header("Grounded")]
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] bool isGrounded = false;
    [SerializeField] LayerMask ground;

    Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //multiply gravity by multiplier
        gravity *= gravityMultiplier;
    }

    void Update()
    {
        //movement
        GroundCheck();
        ApplyGravity();
        OnJump();
        OnMove();
    }
    private void OnMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        characterController.Move(move.normalized * speed * Time.deltaTime);
       
        characterController.Move(velocity);
    }
    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * Time.deltaTime);
    }
    private void ApplyGravity()
    {
        if (!isGrounded)
            velocity.y += gravity * Mathf.Pow(Time.deltaTime, 2);
        else
            velocity.y = 0;
    }
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, characterController.height / 2 + groundDistance, ground))
            isGrounded = true;
        else
            isGrounded = false;
    }
}

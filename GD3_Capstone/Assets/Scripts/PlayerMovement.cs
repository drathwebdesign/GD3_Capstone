using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    private CharacterController characterController;

    [Header("Movement Variables")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 7f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float gravityMultiplier = 2f;

    [Header("Grounded")]
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] public bool isGrounded = false;
    [SerializeField] LayerMask outdoorGround;   // New LayerMask for outdoor
    [SerializeField] LayerMask indoorGround;    // New LayerMask for indoor

    [Header("Bools")]
    public bool isSprinting { get; private set; }
    public bool isIndoors { get; private set; } // New bool to track environment

    Vector3 move;
    Vector3 velocity;
    private float currentSpeed;

    void Start() {
        characterController = GetComponent<CharacterController>();

        isSprinting = false;
        currentSpeed = walkSpeed;

        gravity *= gravityMultiplier;
    }

    void Update() {
        GroundCheck();
        ApplyGravity();
        OnJump();
        OnSprint();
        OnMove();
    }

    private void OnMove() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        move = transform.right * horizontal + transform.forward * vertical;

        characterController.Move(move.normalized * currentSpeed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnSprint() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            currentSpeed = sprintSpeed;
            isSprinting = true;
        } else {
            currentSpeed = walkSpeed;
            isSprinting = false;
        }
    }

    private void OnJump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void ApplyGravity() {
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2.0f;
        else
            velocity.y += gravity * Time.deltaTime;
    }

    private void GroundCheck() {
        // Check for indoor ground
        if (Physics.Raycast(transform.position, -transform.up, characterController.height / 2 + groundDistance, indoorGround)) {
            isGrounded = true;
            isIndoors = true; // Player is indoors
        }
        // Check for outdoor ground
        else if (Physics.Raycast(transform.position, -transform.up, characterController.height / 2 + groundDistance, outdoorGround)) {
            isGrounded = true;
            isIndoors = false; // Player is outdoors
        } else {
            isGrounded = false;
        }
    }

    public Vector3 GetMovementVector() {
        return move;
    }
}

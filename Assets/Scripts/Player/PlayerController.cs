using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator animator;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving;
    private bool isJumping;

    private float xAxis, zAxis;
    private Vector2 moveInput;
    private Vector3 camForward, camRight;
    private Vector3 movement;

    private Quaternion targetRotation;

    private readonly int IsMovingHash = Animator.StringToHash("IsWalking");
    private readonly int JumpHash = Animator.StringToHash("Jump");

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnMove(InputValue _value)
    {
        moveInput = _value.Get<Vector2>();
    }

    private void OnJump(InputValue _value)
    {
        if (_value.isPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger(JumpHash);
        }
    }
    
    private void Update()
    {
        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        xAxis = moveInput.x;
        zAxis = moveInput.y;

        camForward = cameraTransform.forward;
        camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        movement = camForward.normalized * zAxis + camRight.normalized * xAxis;

        isMoving = movement.magnitude > 0.1f && isGrounded;
        animator.SetBool(IsMovingHash, isMoving);

        if(movement.magnitude > 0.1f)
        {
            // Rotate the player to face the movement direction
            targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        controller.Move(movement.normalized * moveSpeed * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Input")]
    [SerializeField] private InputActionAsset actions;

    [Header("Sprint")]
    [SerializeField] private float sprintMultiplier = 1.7f;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float cameraTurnThreshold = 30f;

    private CharacterController controller;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    private Vector2 moveInput;
    private Vector3 velocity;

    private bool isSprinting;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        var map = actions.FindActionMap("Gameplay", true);
        map.Enable();

        // IMPORTANTE si usas Control Schemes
        map.devices = new InputDevice[]
            {
                Keyboard.current,
                Gamepad.current
            };

        moveAction = map.FindAction("Move", true);
        jumpAction = map.FindAction("Jump", true);
        sprintAction = map.FindAction("Sprint", true);

        moveAction.performed += OnMovePerformed;
        moveAction.canceled += _ => moveInput = Vector2.zero;

        jumpAction.performed += OnJumpPerformed;

        sprintAction.performed += _ => isSprinting = true;
        sprintAction.canceled += _ => isSprinting = false;

        Debug.Log("PlayerController input enabled");
    }

    void OnDisable()
    {
        moveAction.performed -= OnMovePerformed;
        jumpAction.performed -= OnJumpPerformed;
        sprintAction.performed -= _ => isSprinting = true;
        sprintAction.canceled -= _ => isSprinting = false;
    }

    void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        if (controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;

        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(forward);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] float jumpStrength;

    [Header("Move")]
    [SerializeField] float walkSpeed;
    [SerializeField] float sneakSpeed;
    [SerializeField] float sprintSpeed;
    bool isSprinting;
    bool isSneaking;
    Vector2 inputVector;
    
    [Header("GroundCast")] 
    [SerializeField] private Transform rayStart;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Components")]
    Rigidbody rb;
    [SerializeField] UILevel uiLevel;

    [Header("SlopeCheck")]
    [SerializeField] float maxWalkableSlopeAngle;

    [Header("Cam")] 
    [SerializeField] Transform cameraFollowTransform;
    [SerializeField] float sensitivity = 1;
    float cameraPitch;
    float cameraRoll;
    [SerializeField] bool invertLookY;
    [SerializeField] Transform visual;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        IsGrounded();

        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    //Lets the camera rotate depending on mouse movement
    void Look()
    {
        if (Time.timeScale != 0)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            cameraPitch += mouseDelta.y * sensitivity;

            cameraRoll += mouseDelta.x * sensitivity;

            if (invertLookY) 
            {
                cameraFollowTransform.localEulerAngles = new Vector3(Mathf.Clamp(cameraPitch, -80, 80), cameraRoll, 0f);
            }
            else
            {
                cameraFollowTransform.localEulerAngles = new Vector3(Mathf.Clamp(-cameraPitch, -80, 80), cameraRoll, 0f);
            }
        }
    }

    //Applies speed according to state of movement and which button is 
    void Move()
    {
        if (!SlopeIsWalkable()) 
            return;
        
        if (isSneaking)
        {
            rb.velocity = ApplySpeed(sneakSpeed);
        }
        else if (isSprinting)
        {
            rb.velocity = ApplySpeed(sprintSpeed);
        }
        else
        {
            rb.velocity = ApplySpeed(walkSpeed);
        }
    }
    
    //Applies speed on the individual vectors and moves in the direction where the player is looking
    Vector3 ApplySpeed(float speed)
    {
        Vector3 movementDir = new Vector3(inputVector.x * speed, rb.velocity.y, inputVector.y * speed);

        movementDir = Quaternion.AngleAxis(cameraFollowTransform.localEulerAngles.y, Vector3.up) * movementDir;

        if (movementDir.x != 0 && movementDir.z != 0)
        {
            var lookDir = movementDir;
            lookDir.y = 0f;
            visual.rotation = Quaternion.LookRotation(lookDir);
        }

        return movementDir;
    }

    //gets the move vector of WASD input
    void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
    }
    
    //Slows down character
    void OnSneak(InputValue inputValue)
    {
        isSneaking = !isSneaking;
    }

    //Lets character sprint
    void OnSprint(InputValue inputValue)
    {
        isSprinting = !isSprinting;
    }
    
    //Lets character jump
    void OnJump(InputValue inputValue)
    {
        if (IsGrounded())
        {
            rb.AddForce(0, inputValue.Get<float>() * jumpStrength, 0, ForceMode.Impulse);
        }
    }

    //pauses the game and unpauses it when already paused
    void OnPause(InputValue inputValue)
    {
        uiLevel.ShowPauseScreen();
    }
    
    //Checks if character is on ground
    bool IsGrounded()
    {
        return Physics.Raycast(rayStart.position, Vector3.down, rayLength, groundLayer);
    }

    //When slopes are too high, they are not walkable anymore
    bool SlopeIsWalkable()
    {
        Physics.Raycast(rayStart.position, Vector3.down, out var hit, rayLength, groundLayer);

        if (hit.collider == null) 
            return true;
        
        var angle = Vector3.Angle(hit.normal, Vector3.up);

        return angle < maxWalkableSlopeAngle;
    }
}
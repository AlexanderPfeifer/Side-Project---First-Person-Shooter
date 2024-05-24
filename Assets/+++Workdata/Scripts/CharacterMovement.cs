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

    [Header("SlopeCheck")]
    [SerializeField] float maxWalkableSlopeAngle;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        IsGrounded();
    }

    void FixedUpdate()
    {
        Move();
    }

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
    
    Vector3 ApplySpeed(float speed)
    {
        return new Vector3(inputVector.x * speed, rb.velocity.y, inputVector.y * speed);
    }

    void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
    }
    
    void OnSneak(InputValue inputValue)
    {
        isSneaking = !isSneaking;
    }

    void OnSprint(InputValue inputValue)
    {
        isSprinting = !isSprinting;
    }
    
    void OnJump(InputValue inputValue)
    {
        if (IsGrounded())
        {
            rb.AddForce(0, inputValue.Get<float>() * jumpStrength, 0, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(rayStart.position, Vector3.down, rayLength, groundLayer);
    }

    bool SlopeIsWalkable()
    {
        Physics.Raycast(rayStart.position, Vector3.down, out var hit, rayLength, groundLayer);

        if (hit.collider == null) 
            return true;
        
        var angle = Vector3.Angle(hit.normal, Vector3.up);

        return angle < maxWalkableSlopeAngle;
    }
}
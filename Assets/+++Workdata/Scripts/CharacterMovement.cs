using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    void OnJump()
    {
        
    }

    void OnSneak(InputValue inputValue)
    {
        Debug.Log(inputValue.Get<float>());
    }

    void OnMove(InputValue inputValue)
    {
        Debug.Log(inputValue.Get<Vector2>());
    }
}
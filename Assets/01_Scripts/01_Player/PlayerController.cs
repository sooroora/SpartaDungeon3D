using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovingStat movingStat;
    [SerializeField] private LayerMask groundLayerMask;
    Rigidbody rb;

    private Vector2 moveInput;
    Vector3 forward;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        this.transform.position +=
            forward * (moveInput.y * movingStat.Speed * Time.deltaTime);

        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        this.transform.position +=
            right * (moveInput.x * movingStat.Speed * Time.deltaTime);
    }

    public void OnJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * movingStat.JumpForce,ForceMode.Impulse);
        }
    }
    
    public void UpdateForward(Vector3 _forward)
    {
        forward = _forward;
    }
    public void UpdateMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };
        
        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}

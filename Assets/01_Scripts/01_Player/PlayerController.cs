using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovingStat movingStat;
    [SerializeField] private LayerMask groundLayerMask;
    Rigidbody rb;

    private Vector2 moveInput;
    Vector3 forward;
    bool isDashing;

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
        float nowSpeed = movingStat.Speed;

        if (isDashing &&
            CharacterManager.Instance.Player.Condition.Stamina.CurrentValue > 0.0f)
        {
            CharacterManager.Instance.Player.Condition.Dash(Time.deltaTime * 5.0f);
            nowSpeed = nowSpeed * movingStat.DashMultiplier;
        }


        this.transform.position +=
            forward * (moveInput.y * nowSpeed * Time.deltaTime);

        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        this.transform.position +=
            right * (moveInput.x * nowSpeed * Time.deltaTime);
    }

    public void OnJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * movingStat.JumpForce, ForceMode.Impulse);
        }
    }

    public void OnDash(bool _isDashing)
    {
        isDashing = _isDashing;
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
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}

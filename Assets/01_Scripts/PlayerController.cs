using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovingStatHandler movingStat;

    public float speed = 5;
    private Vector2 moveInput;
    Vector3 forward;

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        this.transform.position +=
            forward * (moveInput.y * speed * Time.deltaTime);

        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        this.transform.position +=
            right * (moveInput.x * speed * Time.deltaTime);
    }


    
    public void UpdateForward(Vector3 _forward)
    {
        forward = _forward;
    }
    public void UpdateMoveInput(Vector2 input)
    {
        moveInput = input;
    }
}

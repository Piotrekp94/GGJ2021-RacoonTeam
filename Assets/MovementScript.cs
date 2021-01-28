using System;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    [SerializeField] private float speed;

    public Rigidbody rb;
    public Animator animator;
    

    public Vector3 _movement;
    private bool isMoving;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _movement = stepToDesiredSpeed(1f, 0.01f, _movement);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _movement = stepToDesiredSpeed(-1f, 0.01f, _movement);
            isMoving = true;
        }
        else if(isMoving)
        {
            _movement = stepToDesiredSpeed(0, 0.02f, _movement);
        }

        _movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat(Horizontal, _movement.x);
        animator.SetFloat(Vertical, _movement.y);
        animator.SetFloat(Speed, _movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    private Vector3 stepToDesiredSpeed(float desiredSpeed, float step, Vector3 currentSpeed)
    {
        if (Math.Abs(currentSpeed.x - desiredSpeed) < step)
        {
            currentSpeed.x = desiredSpeed;
            if (desiredSpeed == 0)
            {
                isMoving = false;
            }
            return currentSpeed;
        }

        if (currentSpeed.x > desiredSpeed) step *= -1;

        if (step < 0)
        {
            if (currentSpeed.x <= desiredSpeed)
                currentSpeed.x = desiredSpeed;
            else
                currentSpeed.x += step;
        }
        else
        {
            if (currentSpeed.x >= desiredSpeed)
                currentSpeed.x = desiredSpeed;
            else
                currentSpeed.x += step;
        }

        return currentSpeed;
    }
}
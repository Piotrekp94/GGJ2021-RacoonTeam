using System;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int isStoppingHash = Animator.StringToHash("isStopping");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    [SerializeField] private float speed;

    public Rigidbody rb;
    public Animator animator;


    public Vector3 _movement;
    public bool isStopping = false;
    public bool isTurning = false;
    private bool isMoving = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isStopping = false;
            _movement = stepToDesiredSpeed(1f, 0.01f, _movement);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            isStopping = false;
            _movement = stepToDesiredSpeed(-1f, 0.01f, _movement);
            isMoving = true;
        }
        else if (isMoving)
        {
            isStopping = true;
            _movement = stepToDesiredSpeed(0, 0.02f, _movement);
        }

        _movement.y = Input.GetAxisRaw("Vertical");

        animator.SetBool(isStoppingHash, isStopping);
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
            if (desiredSpeed == 0) isMoving = false;
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
using System;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int isStoppingHash = Animator.StringToHash("isStopping");
    private static readonly int isTurningHash = Animator.StringToHash("isTurning");

    [SerializeField] private float speed;

    public Rigidbody rb;
    public Animator animator;

    public bool isRunningLeft;

    public Vector3 _movement;
    public bool isStopping;
    public bool isTurning;
    private bool isMoving;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_movement.x > 0) isRunningLeft = true;
            if (_movement.x < 0)
                isTurning = true;
            else
                isTurning = false;
            isStopping = false;
            _movement = stepToDesiredSpeed(1f, 0.01f, _movement);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_movement.x < 0) isRunningLeft = false;
            if (_movement.x > 0)
                isTurning = true;
            else
                isTurning = false;

            
            isStopping = false;
            _movement = stepToDesiredSpeed(-1f, 0.01f, _movement);
        }
        else if (isMoving)
        {
            isTurning = false;
            isStopping = true;
            _movement = stepToDesiredSpeed(0, 0.01f, _movement);
        }

        if (isRunningLeft)
        {
            if (transform.localScale.z > 0)
            {
                var localScale = transform.localScale;
                localScale =
                    new Vector3(localScale.x, localScale.y, -localScale.z);
                transform.localScale = localScale;
            }
        } else
        if (!isRunningLeft)
        {
            if (transform.localScale.z < 0)
            {
                var localScale = transform.localScale;
                localScale =
                    new Vector3(localScale.x, localScale.y, -localScale.z);
                transform.localScale = localScale;
            }
        }

        _movement.y = Input.GetAxisRaw("Vertical");

        animator.SetBool(isTurningHash, isTurning);
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

        isMoving = true;
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
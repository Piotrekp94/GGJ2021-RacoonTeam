using System;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int isStoppingHash = Animator.StringToHash("isStopping");
    private static readonly int currentStateHash = Animator.StringToHash("currentState");

    [SerializeField] private float speed;

    public Rigidbody rb;
    public Animator animator;

    public bool isRunningLeft;

    public Vector3 _movement;
    public bool isStopping;
    public bool isTurning;
    private bool isMoving;

    private float currentStep = 0.1f;
    private float desiredSpeed = 0f;

    public float maxSpeed = 1.0f;
    public RunningStatesEnum currentState = RunningStatesEnum.Idle;
    public RunningDirections currentDirection = RunningDirections.Right;
    public RunningLockableStatesEnum currentLock = RunningLockableStatesEnum.Unlocked;


    private void Update()
    {
        desiredSpeed = generateDesiredSpeed();
        currentStep = generateDesiredStep();
        _movement = stepToDesiredSpeed(desiredSpeed, currentStep, _movement);
        if (currentLock == RunningLockableStatesEnum.Locked)
        {
            return;
        }
        if (currentState == RunningStatesEnum.Idle)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentState = RunningStatesEnum.Running;
                currentLock = RunningLockableStatesEnum.Locked;
                currentDirection = RunningDirections.Left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                currentState = RunningStatesEnum.Running;
                currentLock = RunningLockableStatesEnum.Locked;
                currentDirection = RunningDirections.Right;
            }
        } else
        if (currentState == RunningStatesEnum.Running)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                // if (currentDirection == RunningDirections.Right)
                // {
                //     currentState = RunningStatesEnum.Turning;
                //     currentLock = RunningLockableStatesEnum.Locked;
                //     currentDirection = RunningDirections.Left;
                // }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                // if (currentDirection == RunningDirections.Left)
                // {
                //     currentState = RunningStatesEnum.Turning;
                //     currentLock = RunningLockableStatesEnum.Locked;
                //     currentDirection = RunningDirections.Right;
                // }
            }
            else
            {
                currentState = RunningStatesEnum.Stopping;
            }
        }
        if (currentState == RunningStatesEnum.Stopping)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && currentDirection == RunningDirections.Right)
            {
                currentState = RunningStatesEnum.Turning;
                currentLock = RunningLockableStatesEnum.Locked;
                currentDirection = RunningDirections.Left;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && currentDirection == RunningDirections.Left)
            {
                currentState = RunningStatesEnum.Turning;
                currentLock = RunningLockableStatesEnum.Locked;
                currentDirection = RunningDirections.Right;
            }
        }

        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     if (_movement.x > 0) isRunningLeft = true;
        //     if (_movement.x < 0)
        //         isTurning = true;
        //     else
        //         isTurning = false;
        //     isStopping = false;
        //     _movement = stepToDesiredSpeed(1f, 0.01f, _movement);
        // }
        // else if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     if (_movement.x < 0) isRunningLeft = false;
        //     if (_movement.x > 0)
        //         isTurning = true;
        //     else
        //         isTurning = false;
        //
        //     
        //     isStopping = false;
        //     _movement = stepToDesiredSpeed(-1f, 0.01f, _movement);
        // }
        // else if (isMoving)
        // {
        //     isTurning = false;
        //     isStopping = true;
        //     _movement = stepToDesiredSpeed(0, 0.01f, _movement);
        // }
        //
        // if (isRunningLeft)
        // {
        //     if (transform.localScale.z > 0)
        //     {
        //         var localScale = transform.localScale;
        //         localScale =
        //             new Vector3(localScale.x, localScale.y, -localScale.z);
        //         transform.localScale = localScale;
        //     }
        // } else
        // if (!isRunningLeft)
        // {
        //     if (transform.localScale.z < 0)
        //     {
        //         var localScale = transform.localScale;
        //         localScale =
        //             new Vector3(localScale.x, localScale.y, -localScale.z);
        //         transform.localScale = localScale;
        //     }
        // }
        //
        // _movement.y = Input.GetAxisRaw("Vertical");
        //
         animator.SetInteger(currentStateHash, (int)currentState);
        // animator.SetBool(isStoppingHash, isStopping);
        // animator.SetFloat(Speed, _movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    private float generateDesiredStep()
    {
        float s = 0f;
        if (RunningStatesEnum.Running == currentState)
        {
            s = 0.1f;

        }
        if (RunningStatesEnum.Stopping == currentState || RunningStatesEnum.Idle == currentState || RunningStatesEnum.Turning == currentState)
        {
            s = 0.2f;
        }

        return s;
    }
    private float generateDesiredSpeed()
    {
        float s = 0f;
        if (RunningStatesEnum.Running == currentState)
        {
            s = maxSpeed;
            if (RunningDirections.Left == currentDirection)
            {
                s = maxSpeed * -1.0f;
            }
        }
        if (RunningStatesEnum.Stopping == currentState || RunningStatesEnum.Idle == currentState || RunningStatesEnum.Turning == currentState)
        {
            s = 0.0f;
        }

        return s;
    }
    private Vector3 stepToDesiredSpeed(float desiredSpeed, float step, Vector3 currentSpeed)
    {
        if (Math.Abs(currentSpeed.x - desiredSpeed) < step)
        {
            currentSpeed.x = desiredSpeed;
            if (desiredSpeed == 0) isMoving = false;
            currentLock = RunningLockableStatesEnum.Unlocked;
            if (currentState == RunningStatesEnum.Stopping)
                currentState = RunningStatesEnum.Idle;
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
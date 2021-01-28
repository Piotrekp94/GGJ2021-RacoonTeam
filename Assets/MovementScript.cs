using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float speed;

    public Rigidbody rb;
    public Animator animator;

    private Vector3 _movement;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _movement = stepToDesiredSpeed(1, 0.01f, _movement);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            _movement = stepToDesiredSpeed(1, 0.01f, _movement);
        }
        else
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

    Vector3 stepToDesiredSpeed(float desiredSpeed, float step, Vector3 currentSpeed)
    {
        if (currentSpeed.x > desiredSpeed)
        {
            step *= -1;
        }

        if (step < 0)
        {
            if (currentSpeed.x <= desiredSpeed)
            {
                currentSpeed.x = desiredSpeed;
            }
            else
            {
                currentSpeed.x += step;
            }
        }
        else
        {
            if (currentSpeed.x >= desiredSpeed)
            {
                currentSpeed.x = desiredSpeed;
            }
            else
            {
                currentSpeed.x += step;
            }
        }

        return currentSpeed;
    }
}
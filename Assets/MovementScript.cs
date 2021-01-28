using System;
using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_movement.x >= 1)
            {
                _movement.x = 1;
            }
            else
            {
                _movement.x += 0.10f;
            }
        }
        else
        {
            if (_movement.x <= 0)
            {
                _movement.x = 0;
            }
            else
            {
                _movement.x -= 0.10f;
            }
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
}

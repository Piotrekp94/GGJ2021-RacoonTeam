using System;
using UnityEditor;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private static readonly int currentSpeed = Animator.StringToHash("currentSpeed");

    [SerializeField] private int speed;

    public Rigidbody rb;
    public Animator animator;
    public bool isOnGround;
    
    public Vector3 _movement;


    private float currentStep = 0.1f;
    private float desiredSpeed = 0f;
    private bool isMoving;


    private void Update()
    {
        var speedx = speed;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speedx = -1 * speedx;
            transform.eulerAngles  = new Vector3(transform.rotation.x, 90f, transform.rotation.z);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles  = new Vector3(transform.rotation.x, -90f, transform.rotation.z);
        }
        else
        {
            speedx = 0;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
             
        }


        _movement.x = speedx;
        animator.SetInteger(currentSpeed, speedx);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        isOnGround = true;
    }
    private void OnCollisionExit(Collision other)
    {
        isOnGround = false;
    }
}
using System;
using UnityEditor;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private static readonly int currentSpeedHash = Animator.StringToHash("currentSpeed");
    private static readonly int isOnGroundHash = Animator.StringToHash("isOnGround");


    [SerializeField] private int speed;

    public Rigidbody rb;
    public Animator animator;
    public bool isOnGround = false;
    
    public Vector3 _movement;


    private float currentStep = 0.1f;
    private float desiredSpeed = 0f;
    private bool isMoving;
    public int speedy = 0;


    private void Update()
    {
        var speedx = speed;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speedx = -1 * speedx;
            transform.eulerAngles  = new Vector3(0, 90f, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles  = new Vector3(0, -90f, 0);
        }
        else
        {
            speedx = 0;
        }
        
        if (isOnGround &&Input.GetKey(KeyCode.Space))
        {
            transform.eulerAngles  = new Vector3(0, transform.rotation.y, 0);
            speedy = 10;
        }

        if (speedy > 0)
        {
            speedy -= 1;
        }


        _movement.x = speedx;
        _movement.y = speedy;
        animator.SetInteger(currentSpeedHash, speedx);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        isOnGround = true;
        animator.SetBool(isOnGroundHash, isOnGround);

    }
    private void OnCollisionExit(Collision other)
    {
        isOnGround = false;
        animator.SetBool(isOnGroundHash, isOnGround);
    }
}
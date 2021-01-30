using System;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private static readonly int currentSpeedHash = Animator.StringToHash("currentSpeed");
    private static readonly int isOnGroundHash = Animator.StringToHash("isOnGround");


    [SerializeField] private int speed;

    public Rigidbody rb;
    public Animator animator;
    public bool isOnGround;
    public bool wasDoubleJumpUsed;

    public Vector3 _movement;

    public int speedy;
    public Vector3 jump;
    public Vector3 dash;

    public float jumpForce = 2.0f;
    public float dashForce = 2.0f;


    private void Start()
    {
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void Update()
    {
        var speedx = speed;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speedx = -1 * speedx;
            transform.eulerAngles = new Vector3(0, 90f, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, -90f, 0);
        }
        else
        {
            speedx = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !wasDoubleJumpUsed)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            wasDoubleJumpUsed = true;
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject[] photos = GameObject.FindGameObjectsWithTag("IsPhotographic");

            foreach (GameObject photo in photos)
            {
                photo.GetComponent<PhotogenicScript>().beVisible();
            }
        }


        _movement.x = speedx;
        _movement.y = speedy;
        if (Input.GetKeyDown(KeyCode.D))
        {
            dash = _movement * dashForce;
            rb.AddForce(dash, ForceMode.Impulse);
        }
        animator.SetInteger(currentSpeedHash, speedx);
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = (rb.position + _movement * (speed * Time.fixedDeltaTime));
        Vector3 direction = desiredPosition - rb.position;
        Ray ray = new Ray(rb.position, direction);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, direction.magnitude))
            rb.MovePosition(desiredPosition);
        else
            rb.MovePosition(hit.point);
        // rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    private void OnCollisionExit(Collision other)
    {
        isOnGround = false;
        animator.SetBool(isOnGroundHash, isOnGround);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision Enter");
        Deadly d = other.gameObject.GetComponent<Deadly>();
        if (d != null)
        {
            Debug.Log("Collision Ded");

            Application.Quit();
        }
    }

    private void OnCollisionStay()
    {
        isOnGround = true;
        wasDoubleJumpUsed = false;
        animator.SetBool(isOnGroundHash, isOnGround);
    }
}
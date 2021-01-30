using System;
using TMPro;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private static readonly int currentSpeedHash = Animator.StringToHash("currentSpeed");
    // private static readonly int isOnGroundHash = Animator.StringToHash("isOnGround");


    [SerializeField] private int speed;

    public Rigidbody rb;
    public Animator animator;
    public bool isOnGround;
    public bool wasDoubleJumpUsed;

    private  Vector3 _movement;

    private int speedy;
    private Vector3 jump;
    private Vector3 dash;

    public float jumpForce = 2.0f;
    public float dashForce = 2.0f;

    public GameObject flashGo;
    private GameObject instantiedFlash;

    public AudioSource audioSource;
    public float flashCooldown = 5f;
    private bool flashReady = true;

    private void Start()
    {
        instantiedFlash = Instantiate(flashGo);
        instantiedFlash.SetActive(false);
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
        

        if (Input.GetKeyDown(KeyCode.P) && flashReady)
        {
            flashReady = false;
            GameObject[] photos = GameObject.FindGameObjectsWithTag("IsPhotographic");
            instantiedFlash.SetActive(true);
            Invoke("endFlash", 0.15f);
            Invoke("refreshFlash", flashCooldown);

            audioSource.Play();
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

    private void endFlash()
    {
        instantiedFlash.SetActive(false);

    }
    
    private void refreshFlash()
    {
        flashReady = true;
    }
    private void OnCollisionExit(Collision other)
    {
        isOnGround = false;
        // animator.SetBool(isOnGroundHash, isOnGround);
    }



    private void OnCollisionStay()
    {
        isOnGround = true;
        wasDoubleJumpUsed = false;
        // animator.SetBool(isOnGroundHash, isOnGround);
    }
}
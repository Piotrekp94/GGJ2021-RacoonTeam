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

    public float jumpForce = 2.0f;
    public float dashForce = 2.0f;

    public GameObject flashGo;

    public AudioSource audioSource;

    public float flashCooldown = 5f;

    private Vector3 _movement;
    private Vector3 dash;

    private bool dashReady = true;
    private bool flashReady = true;
    private GameObject instantiedFlash;
    private Vector3 jump;

    private int speedy;

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
            var photos = GameObject.FindGameObjectsWithTag("IsPhotographic");
            instantiedFlash.SetActive(true);
            Invoke("endFlash", 0.15f);
            Invoke("refreshFlash", flashCooldown);

            audioSource.Play();
            foreach (var photo in photos) photo.GetComponent<PhotogenicScript>().beVisible();
        }


        _movement.x = speedx;
        _movement.y = speedy;
        if (Input.GetKeyDown(KeyCode.D) && dashReady && !isOnGround)
        {
            dash = _movement * dashForce;
            rb.AddForce(dash, ForceMode.Impulse);
            dashReady = false;
        }

        animator.SetInteger(currentSpeedHash, speedx);
    }

    private void FixedUpdate()
    {
        var desiredPosition = rb.position + _movement * (speed * Time.fixedDeltaTime);
        var direction = desiredPosition - rb.position;
        var ray = new Ray(rb.position, direction);
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
        // animator.SetBool(isOnGroundHash, isOnGround);
    }


    private void OnCollisionStay()
    {
        dashReady = true;
        isOnGround = true;
        wasDoubleJumpUsed = false;
        // animator.SetBool(isOnGroundHash, isOnGround);
    }

    private void endFlash()
    {
        instantiedFlash.SetActive(false);
    }

    private void refreshFlash()
    {
        flashReady = true;
    }
}
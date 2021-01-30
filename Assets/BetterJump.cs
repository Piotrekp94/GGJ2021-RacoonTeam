using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float jumpspeed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * (Physics.gravity.y * ((jumpspeed - 1)) * Time.deltaTime);
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * (Physics.gravity.y * ((jumpspeed - 1)) * Time.deltaTime);
        }
    }
}

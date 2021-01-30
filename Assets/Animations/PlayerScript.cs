using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 lastCheckPoint;
    
    void Start()
    {
        lastCheckPoint = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            lastCheckPoint = other.transform.position;
            Destroy(other.gameObject);
            return;
        }
        if (other.gameObject.CompareTag("Deadly"))
        {
            goToLastCheckPoint();
        } 
        if (other.gameObject.CompareTag("EndLevel"))
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void goToLastCheckPoint()
    {
        transform.position = lastCheckPoint;
    }
}

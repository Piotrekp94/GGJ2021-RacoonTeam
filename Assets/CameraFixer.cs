using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixer : MonoBehaviour
{
    public int cameraDistance;
    public GameObject targetToFollow;

    public int yoffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostion = targetToFollow.transform.position;
        this.transform.position = new Vector3(targetPostion.x, targetPostion.y + yoffset, targetPostion.z - cameraDistance);
    }
}

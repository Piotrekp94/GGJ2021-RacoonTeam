using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotogenicScript : MonoBehaviour
{
    public int time;
    public float alpha = -1f;
    public BoxCollider Collider;

    public void Start()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        
    }

    public void beVisible()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        alpha = 1.0f;
    }

    public void Update()
    {
        if (alpha < 0)
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 0.0f, alpha);

        }
        else
        {
            alpha -= 0.01f;
            this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }



        
    }

}

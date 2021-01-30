using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PhotogenicScript : MonoBehaviour
{
    public int time;
    public float alpha = 0f;
    public BoxCollider Collider;

    public void Start()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        
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
            alpha -= ((float) 1/time)*Time.deltaTime;
            this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }



        
    }

}

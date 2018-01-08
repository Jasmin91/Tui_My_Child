using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TUIO;

public class movement : MonoBehaviour
{

    public float speed = 15;
    public string axis = "Vertical";
    public int markerID = 0;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Test");
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
    }
}

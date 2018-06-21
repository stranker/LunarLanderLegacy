using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

    private Rigidbody2D rd;
    private Vector3 rotationVector = Vector3.zero;
    public float speed = 50;
    public float rotationSpeed = 40;
    public float gravity;
    public int maxRotation = 90;


    // Use this for initialization
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = new Vector2(0.5f, -0.1f);
        rotationVector = transform.rotation.eulerAngles;
        rd.gravityScale = gravity;
    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetAxis("Vertical") > 0.1f)
        {
            rd.AddForce(transform.up * speed * Time.deltaTime);
            GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            rotationVector.z += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            rotationVector.z = Mathf.Clamp(rotationVector.z, -maxRotation, maxRotation);
        }
        transform.eulerAngles = rotationVector;
    }
}

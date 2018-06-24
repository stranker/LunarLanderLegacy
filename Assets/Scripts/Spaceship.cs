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
    public int fuel;
    public int MAX_FUEL;
    public Vector2 velocity;
    public float altitude;
    public float fuelTime;
    public float fuelConsumptionTime;
    public LayerMask terrainLayer;
    private const int altitudeMagnitudeConverter = 10;
    public Vector2 initialVelocity;
    private bool landed = false;


    // Use this for initialization
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = initialVelocity;
        rotationVector = transform.rotation.eulerAngles;
        rd.gravityScale = gravity;
        MAX_FUEL = 500;
        fuel = MAX_FUEL;
        fuelConsumptionTime = 0.5f;
    }


    private void FixedUpdate()
    {
        Movement();
        FuelControl();
        AltitudeControl();
        GroundControl();
    }

    private void AltitudeControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, terrainLayer);
        if (hit.collider.tag == "Terrain")
        {
            altitude = (GetComponent<SpriteRenderer>().bounds.min.y - hit.point.y) * altitudeMagnitudeConverter;
        }
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2), -Vector3.up * 10, Color.red);
    }

    private void GroundControl()
    {
        Vector2 spriteSize = GetComponent<SpriteRenderer>().bounds.size;
        Vector2 leftRayPos = new Vector2(transform.position.x - spriteSize.x / 2, transform.position.y - spriteSize.y / 2);
        Vector2 rightRayPos = new Vector2(transform.position.x + spriteSize.x / 2, transform.position.y - spriteSize.y / 2);
        RaycastHit2D leftRay = Physics2D.Raycast(leftRayPos, -transform.up, 0.1f, terrainLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(leftRayPos, -transform.up, 0.1f, terrainLayer);
        if (leftRay && rightRay && !landed)
        {
            if (Mathf.Abs(velocity.x) < 5 && Mathf.Abs(velocity.y) < 5)
            {
                landed = true;
            }
        }
    }

    private void Movement()
    {
        if (Input.GetAxis("Vertical") > 0.1f)
        {
            rd.AddForce(transform.up * speed * Time.deltaTime);
            GetComponent<ParticleSystem>().Play();
            fuelTime += Time.deltaTime;
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
        velocity = rd.velocity * speed;
    }

    private void FuelControl()
    {
        if (fuelTime >= fuelConsumptionTime)
        {
            fuel--;
            fuelTime = 0;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

    public float speed = 50;
    public float rotationSpeed = 40;
    public float gravity;
    public int maxRotation = 90;
    public int fuel;
    public int MAX_FUEL = 800;
    public Vector2 velocity;
    public float altitude;
    public float fuelTime;
    public float fuelConsumptionTime = 0.2f;
    public LayerMask terrainLayer;
    public Vector2 initialVelocity;
    public bool alive = true;
    public bool landed = false;

    private const int altitudeMagnitudeConverter = 10;
    private int decreaseFuelOnDeath = 200; private Rigidbody2D rd;
    private Vector3 rotationVector = Vector3.zero;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = initialVelocity;
        rotationVector = transform.rotation.eulerAngles;
        rd.gravityScale = gravity;
        fuel = MAX_FUEL;
    }

    private void FixedUpdate()
    {
        if (alive && !landed)
        {
            if (fuel > 0)
            {
                Movement();
            }
            FuelControl();
            AltitudeControl();
            GroundControl();
        }
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
        RaycastHit2D leftRay = Physics2D.Raycast(leftRayPos, -transform.up, 0.2f, terrainLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(rightRayPos, -transform.up, 0.2f, terrainLayer);
        if (leftRay && rightRay && !landed && alive)
        {
            if (Mathf.Abs(velocity.x) < 15 && Mathf.Abs(velocity.y) < 20)
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
        if (fuel <= 0)
        {
            fuel = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Terrain")
        {
            if (!landed && alive)
            {
                Kill();
            }
        }
    }

    private void Kill()
    {
        alive = false;
        fuel -= decreaseFuelOnDeath;
    }

    public bool GetAlive()
    {
        return alive;
    }

    public void SetAlive(bool val)
    {
        alive = val;
    }

    public int GetFuelDecreaseOnDeath()
    {
        return decreaseFuelOnDeath;
    }

    public bool IsLanded()
    {
        return landed;
    }

}

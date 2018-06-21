using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {

    private Rigidbody2D rd;
    public float speed = 2;
    public Vector2 velocity = Vector2.zero;
    public Vector2 rdVelocity;
    public Vector2 vec;

	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = new Vector2(0.5f, -0.1f);
	}
	
	// Update is called once per frame
	void Update () {
      
        velocity.y = Input.GetAxis("Vertical");
        rd.velocity += (transform.up * velocity).normalized * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(transform.rotation.x,transform.rotation.y, transform.rotation.z + 5));
        }
        rdVelocity = rd.velocity;
    }
}

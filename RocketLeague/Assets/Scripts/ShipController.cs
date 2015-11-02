﻿using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    Rigidbody rb;

    float mSpeed;
    float mRotationSpeed;
    Vector3 mDirection;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        mSpeed = 30;
        mRotationSpeed = 75;
	}
	
	// Update is called once per frame
	void Update () {
        InputHandling();
        CheckIfScrewedUpAngle();
    }

    void CheckIfScrewedUpAngle()
    {
        //FipOver

        if(transform.rotation.z > 1 && transform.position.y < 6)
        {
            Debug.Log("FLIP");
            transform.Rotate(Vector3.forward, 180);
        }
    }

    void ApplyForce(Vector3 force)
    {

    }


    void InputHandling()
    {
        float translation = Input.GetAxis("Vertical") * mSpeed;
        float rotation = Input.GetAxis("Horizontal") * mRotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        mDirection = new Vector3(0, rotation, translation);

        if(Input.GetKey("space"))
        {
            Debug.Log("O)Ut");
            rb.AddForce(100 * Vector3.up);
        }
    }

    void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.name == "Ball")
        {
            other.rigidbody.AddForceAtPosition(10 * mDirection, transform.position, ForceMode.Impulse);
        }
        
    }
}

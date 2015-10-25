using UnityEngine;
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
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Ball")
        {
            other.rigidbody.AddForceAtPosition(50 * mDirection, transform.position, ForceMode.Impulse);
        }
    }
}

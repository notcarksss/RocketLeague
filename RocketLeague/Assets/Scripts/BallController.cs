using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
    Rigidbody rb;

    float bouncyness = 1.3f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            rb.velocity += 10* coll.gameObject.GetComponent<Rigidbody>().velocity;
        }       
        rb.velocity = new Vector3(rb.velocity.x, coll.relativeVelocity.y / bouncyness, rb.velocity.z);                
    }

}

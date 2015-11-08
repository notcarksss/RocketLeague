using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipController : MonoBehaviour {

    Rigidbody rb;

    float mSpeed;
    float mRotationSpeed;
    Vector3 mDirection;

    bool hasRocket = false;
    float rocketTimeLeft = 0;
    float rocketTime = 7.0f;    


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        mSpeed = 45;
        mRotationSpeed = 75;
	}
	
	// Update is called once per frame
	void Update () {
        InputHandling();
        CheckIfScrewedUpAngle();
        CheckRocketFuelLeft();

        GameObject.Find("PowerUpInd").GetComponent<Text>().text = "Rocket Fuel Time: " + (Mathf.Round(rocketTimeLeft * 100) / 100).ToString();

    }

    void CheckRocketFuelLeft()
    {
        if(rocketTimeLeft > 0)
        {
            rocketTimeLeft -= Time.deltaTime;
        }
        else
        {
            rocketTimeLeft = 0;
            hasRocket = false;
        }
        
    }

    void CheckIfScrewedUpAngle()
    {
        //FipOver

        if(transform.rotation.eulerAngles.z > 1)
        {
            //Debug.Log("FLIP");

            Quaternion rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5.0f * Time.deltaTime);           
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

        if(Input.GetKey("space") && hasRocket)
        {           
            rb.AddForce(45 * Vector3.up, ForceMode.Impulse);
            hasRocket = false;
            rocketTimeLeft = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {    
        
        if (other.gameObject.tag == "PowerUp")
        {     
            rocketTimeLeft = rocketTime;
            hasRocket = true;
        }
        
    }
}

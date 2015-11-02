using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    float timeToReset = 3.0f;
    float goalCoolDown;
    bool goalHit;

    Vector3 ballRestPos;
    GameObject ball;

	// Use this for initialization
	void Start () {
        goalCoolDown = 0;
        goalHit = false;
        ball = null;

        ballRestPos = new Vector3(-7.1f, 20.2f, -50);
    }
	
	// Update is called once per frame
	void Update () {
        if (goalHit)
        {
            if (goalCoolDown > 0)
            {
                goalCoolDown -= Time.deltaTime;
            }
            else
            {
                ResetStuff();
            }
        }
	}

    void ResetStuff()
    {
        gameObject.layer = 9;
        ball.transform.position = ballRestPos;
        goalHit = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Goal");
            goalHit = true;
            goalCoolDown = timeToReset;
            gameObject.layer = 10;
            ball = other.gameObject;    
        }
    }
}

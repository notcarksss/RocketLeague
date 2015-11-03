using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Goal : MonoBehaviour {

    float timeToReset = 3.0f;
    float goalCoolDown;
    bool goalHit;

    Vector3 ballRestPos;
    GameObject ball;


    float globalTImer;   

	// Use this for initialization
	void Start () {
        goalCoolDown = 0;
        goalHit = false;
        ball = null;
        ballRestPos = new Vector3(-7.1f, 20.2f, -50);

        globalTImer = 60.0f;
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

        GameObject.Find("Timer").GetComponent<Text>().text = "Time Remaining: " + (Mathf.Round(globalTImer * 100)/100).ToString();

        if(globalTImer > 0)
        {
            globalTImer -= Time.deltaTime;
        }
        else
        {
            Application.LoadLevel(0);
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Goal : MonoBehaviour {

    float timeToReset = 3.0f;
    float goalCoolDown;
    bool goalHit;

    Vector3 ballRestPos;
    GameObject ball;
    int nbGoals;

    [SerializeField]
    public GameObject GoalText;

    float globalTImer;   

	// Use this for initialization
	void Start () {
        goalCoolDown = 0;
        goalHit = false;
        ball = null;
        ballRestPos = new Vector3(-7.1f, 20.2f, -50);

        globalTImer = 60.0f;
        nbGoals = 0;

        //GameObject.Find("GOAL").GetComponent<Text>().text = "";
    }
	
	// Update is called once per frame
	void Update () {
        if (goalHit)
        {
            if (goalCoolDown > 0)
            {
                GoalText.GetComponent<Text>().text = "GOAL";
                goalCoolDown -= Time.deltaTime;
            }
            else
            {
                ResetStuff();
            }
        }

        GameObject.Find("Timer").GetComponent<Text>().text = "Time Remaining: " + (Mathf.Round(globalTImer * 100)/100).ToString();
        GameObject.Find("GoalCount").GetComponent<Text>().text = "Goals: " + nbGoals.ToString();


        if (globalTImer > 0)
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
        GoalText.GetComponent<Text>().text = "";
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Goal");
            goalHit = true;
            nbGoals++;
            goalCoolDown = timeToReset;
            gameObject.layer = 10;
            ball = other.gameObject;    
        }
    }
}

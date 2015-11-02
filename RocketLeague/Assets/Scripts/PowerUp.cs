using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    public GameObject powerUp;
    bool holdingPowerUp;
    float powerUpDelay;

    float recycleTime;
    Vector3 verticalOffset;

    GameObject pUpInstance;

	// Use this for initialization
	void Start () {
        holdingPowerUp = true;
        powerUpDelay = 3.0f;
        verticalOffset = new Vector3(0, 5, 0);
        SpawnPower();
    }
	
	// Update is called once per frame
	void Update () {
        recyclePower();
    }

    void SpawnPower()
    {
        pUpInstance = (GameObject)Instantiate(powerUp, transform.position + verticalOffset, Quaternion.identity);
        pUpInstance.transform.localScale = pUpInstance.transform.localScale * 30;
        holdingPowerUp = true;
    }

    void recyclePower()
    {
        if (!holdingPowerUp)
        {
            if(recycleTime > 0)
            {
                recycleTime -= Time.deltaTime;
            }
            else
            {
                SpawnPower();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player" && holdingPowerUp)
        {
            Debug.Log("PowerUp");
            holdingPowerUp = false;
            recycleTime = powerUpDelay;
            Destroy(pUpInstance);
        }
        
    }
}

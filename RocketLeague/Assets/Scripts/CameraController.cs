using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [SerializeField]
    GameObject player;

    float stepOver;
    float mSpeed;

    void Start()
    {
        mSpeed = 5.0f;
    }

	
	// Update is called once per frame
	void Update () {
        TranslateToPlayer();
    }

    void TranslateToPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        transform.Translate(direction * mSpeed);

    }
}

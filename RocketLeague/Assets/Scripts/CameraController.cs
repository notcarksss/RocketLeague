using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float smoothness;
    [SerializeField]
    Transform focusTarget;
    Vector3 relativePosition;
    Vector3 desiredPosition;

    [SerializeField]
    int maxInterpolations;

    void Awake()
    {
        relativePosition = transform.position - focusTarget.position;
    }

    void LateUpdate()
    {
        SmoothFollow();
    }


    void SmoothFollow()
    {
        var height = 5.0f;
        var heightDamping = 2.0f;
        var rotationDamping = 3.0f;
        var distance = 25.0f;

        // Calculate the current rotation angles
        float wantedRotationAngle = focusTarget.eulerAngles.y;
        float wantedHeight = focusTarget.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = focusTarget.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(focusTarget);
    }
        
}



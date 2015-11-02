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


    /*

    void FixedUpdate()
    {
        // Check what camera position is best
        List<Vector3> camPositions = CalculatePositions();
        foreach (Vector3 p in camPositions)
        {
            if (CheckLineOfSight(p))
            {
                desiredPosition = p;
                break;
            }
        }

        // TODO: Smoothly translate the camera over to desired position

        //       One way of doing it:
        //       - Use Vector3.Lerp to go from "transform.position" to "desiredPosition".
        //       - Use the "smoothness" variable and Time.deltaTime to make it smooth

        // Can be done in 1 line of code:
        transform.position = Vector3.Lerp(transform.position, focusTarget.position, smoothness * Time.deltaTime);


        // TODO: Smoothly rotate camera to look at target

        //       One way of doing it:
        //       - Use Quaternion.LookRotation to obtain the desired rotation Quaternion (name it "desiredRotation")
        //       - Use Quaternion.Lerp to smoothly go from "transform.rotation" to the desired rotation "desiredRotation"
        //       - Use the "smoothness" variable with Time.deltaTime to make it smooth

        // Can be done in 2-3 lines of code
        // ... = Quaternion.LookRotation(...)
        // transform.rotation = Quaternion.Lerp(...)

        // Create a vector from the camera towards the player.
        Vector3 relPlayerPosition = focusTarget.position - transform.position;

        // Create a rotation based on the relative position of the player being the forward vector.
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);

        // Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smoothness * Time.deltaTime);

    }

    */

    List<Vector3> CalculatePositions()
    {
        Vector3 abovePosition = focusTarget.position + Vector3.up * relativePosition.magnitude;
        Vector3 standardPosition = focusTarget.position + relativePosition;

        List<Vector3> camPositions = new List<Vector3>();
        camPositions.Add(standardPosition);
        for (int i = 0; i < maxInterpolations; i++)
        {
            float t = (float)i / maxInterpolations;
            camPositions.Add(Vector3.Lerp(standardPosition, abovePosition, t));
        }
        camPositions.Add(abovePosition);

        return camPositions;


       

    }

    bool CheckLineOfSight(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, focusTarget.position - position, out hit, relativePosition.magnitude))
        {
            if (hit.transform != focusTarget)
            {
                return false;
            }
        }
        return true;
    }

    public void FocusTarget(Transform newTarget)
    {
        focusTarget = newTarget;
    }

    public Transform GetTarget()
    {
        return focusTarget;
    }
}



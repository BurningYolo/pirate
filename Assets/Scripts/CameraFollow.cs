using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // Smoothing factor for camera movement
    public Vector3 offset;  // Offset from the player's position
    public GameObject secondcamera;



    private void Start()
    {
        secondcamera.SetActive(false); 
    }

    private void LateUpdate()
    {
        // Check if there is a target (current obstacle)
        if (target != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = target.position + offset;
            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Update the camera's position
            transform.position = smoothedPosition;

        }
    }
}

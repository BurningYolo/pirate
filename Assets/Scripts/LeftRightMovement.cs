using UnityEngine;

public class LeftRightMovement : MonoBehaviour
{
    public float leftLimit = -5f; // Left limit of the movement
    public float rightLimit = 5f; // Right limit of the movement
    public float movementSpeed = 2f; // Speed of the movement

    private void Update()
    {
        float newPosition = Mathf.PingPong(Time.time * movementSpeed, rightLimit - leftLimit) + leftLimit;
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    }
}

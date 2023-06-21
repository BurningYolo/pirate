using UnityEngine;

public class SpinAnimationz : MonoBehaviour
{
    
    public float rotationSpeed = 100f; // Adjust the rotation speed as needed

    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
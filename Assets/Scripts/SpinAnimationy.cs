using UnityEngine;

public class SpinAnimationy : MonoBehaviour
{
    public float spinSpeed = 300; // Speed of rotation in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the y-axis
        transform.Rotate(new Vector3(0f, spinSpeed * Time.deltaTime, 0f));
    }
}

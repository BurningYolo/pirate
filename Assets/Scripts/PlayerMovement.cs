/*using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float horizontalSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Move the player forward automatically
        rb.velocity = new Vector3(0, 0, forwardSpeed);

        // Move the player horizontally based on user input
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity += new Vector3(horizontalInput * horizontalSpeed, 0, 0);
    }
}
*/

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float controlSpeed = 5f;
    public float forwardSpeed = 5f;

    private bool isTouching = false;
    private float touchPosX = 0f;
    public GameObject playerGameObject;

    // Define the boundaries for horizontal movement
    public float minXBoundary = -2.5f;
    public float maxXBoundary = 4.18f;

    private void Update()
    {
        GetInput();
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Apply constant forward movement
        Vector3 forwardMovement = transform.forward * forwardSpeed * Time.deltaTime;
        playerGameObject.transform.position += forwardMovement;

        if (isTouching)
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            float horizontalMovement = horizontalInput * controlSpeed * Time.deltaTime;

            // Update the target position for horizontal movement
            touchPosX += horizontalMovement;

            // Restrict the touch position within the defined boundaries
            touchPosX = Mathf.Clamp(touchPosX, minXBoundary, maxXBoundary);

            // Apply horizontal movement to the player's position
            Vector3 newPosition = playerGameObject.transform.position;
            newPosition.x = touchPosX;
            playerGameObject.transform.position = newPosition;
        }
    }

    private void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isTouching)
            {
                // Reset the touch position when the mouse button is pressed
                touchPosX = playerGameObject.transform.position.x;
            }
            isTouching = true;
        }
        else
        {
            isTouching = false;
        }
    }
}









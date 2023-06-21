using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject[] children; // Array to hold references to the children GameObjects
    private int currentChildIndex = 0; // Index of the currently active child
    public ObjectShaker objectShaker;
    private bool obstacle1Triggered = false; // Check if Obstacle1 trigger has occurred
    private bool obstacle2Triggered = false; // Check if Obstacle2 trigger has occurred
    public GameObject particle1;
    public GameObject particle2;
    public GameObject player;
    public float zOffset = -10f; // Adjust this value to set the desired amount to move back in the z-axis
    public GameObject particlePrefab;
    public PlayerHealth playerhealth;
    public SpinAnimationy spinanimation; 
    private void Start()
    {
        spinanimation.enabled = false; 
        particle1.SetActive(false);
        particle2.SetActive(false);
        ActivateChild(currentChildIndex); // Activate the first child on game start
        DeactivateChildrenExcept(currentChildIndex); // Deactivate all other children
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle1") && !obstacle1Triggered)
        {
            obstacle1Triggered = true;
            particle1.SetActive(true); 
            Destroy(children[currentChildIndex]); 
            currentChildIndex++;
            ActivateChild(currentChildIndex);
            objectShaker.ShakeObject(children[currentChildIndex]);
            objectShaker.ShakeObject(player);

        }
        if (other.CompareTag("Obstacle2") && !obstacle2Triggered)
        {
            obstacle2Triggered = true; // Set the Obstacle2 trigger to true
            Destroy(children[currentChildIndex]); // Destroy the current child
            particle2.SetActive(true);
            currentChildIndex++;
            ActivateChild(currentChildIndex); // Activate the next child
            objectShaker.ShakeObject(children[currentChildIndex]);
            objectShaker.ShakeObject(player);
        }


        if (other.CompareTag("hurdle_back"))
        {
            StartCoroutine(SendPlayerBack());
            Debug.Log("Player sent back");
        }

        if (other.CompareTag("cannonballs"))
        {
            StartCoroutine(SendPlayerBack());

            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            
            playerhealth.TakeDamage(1f);
        }



    }

    private System.Collections.IEnumerator SendPlayerBack()
    {
        Vector3 startPosition = player.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + zOffset);
        float duration = 1.0f; // Adjust the duration of the movement

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPosition; // Ensure the player reaches the exact target position
        
    }

    private void ActivateChild(int index)
    {
        children[index].SetActive(true); // Activate the child at the given index
    }

    private void DeactivateChildrenExcept(int index)
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (i != index)
            {
                children[i].SetActive(false); // Deactivate children except the one at the given index
            }
        }
    }
}

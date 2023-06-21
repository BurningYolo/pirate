using UnityEngine;
using DG.Tweening;

public class BoatCollision : MonoBehaviour
{
    public PlayerMovement playerMovementScript;
    public ParticleSystem particlePrefab; // Particle system prefab to activate
    public GameObject[] children; // Array to hold references to the children GameObjects
    public bool obstacleTriggered3 = false;
    public bool obstacleTriggered4 = false;
    public bool obstacleTriggered5 = false;
    public bool obstacleTriggered6 = false;
    public bool obstacleTriggered7 = false;
    public bool obstacleTriggered8 = false;
    public bool hurdleTrigger = false;
    public GameObject[] particles;
    public ObjectShaker objectShaker;
    public Texture2D newTexture;
    public GameObject player;
    public GameObject boat;
    public GameObject hurdle;
    public GameObject hurdle1;
    public GameObject hurdle2;
    public Transform childTransform;
    private Vector3 initialPosition; // Variable to store the initial position of the player
    public Rigidbody playerRigidbody;
    public float jumpForce = 100f;
    public float jumpDuration = 110f;
    private int originalYConstraintValue; // Declare originalYConstraintValue
    public float rotationSpeed = 90f;
    public float targetRotation = -90f;
    public ShootingScript shootingscript; 
    public float zOffset = -10f; // Adjust this value to set the desired amount to move back in the z-axis
    public GameObject firstcamera;
    public GameObject secondcamera;
    public BulletSpawner bulletspawner1;
    public BulletSpawner bulletspawner2;
    public BulletSpawner bulletspawner3;
    public PlayerHealth playerhealth; 


    private void Start()
    {
        shootingscript.enabled = false;
        initialPosition = transform.position; // Store the initial position of the player
        ActivateFirstChildren(); // Activate the first three children on game start
        DeActivateRemainingChildren(); // Activate the remaining children
        originalYConstraintValue = (int)playerRigidbody.constraints; // Assign originalYConstraintValue
    }

    public void DeActivateAllParticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(false);
        }
    }

    private void ActivateFirstChildren()
    {
        children[0].SetActive(true); // Activate the first three children
    }

    private void DeActivateRemainingChildren()
    {
        for (int i = 1; i < children.Length; i++)
        {
            children[i].SetActive(false); // Deactivate the remaining children
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle_Paint"))
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material material = renderer.material;
                if (material != null)
                {
                    material.mainTexture = newTexture;
                }
            }
            
            particles[9].SetActive(true);
            ScalePlayer();
        }

        if (other.CompareTag("Obstacle3") && !obstacleTriggered3)
        {
            childTransform = children[1].transform;
            obstacleTriggered3 = true;
            children[1].SetActive(true);
            ScalePlayer();
            ActivateParticle();
        }

        if (other.CompareTag("Obstacle4") && !obstacleTriggered4)
        {
            childTransform = children[2].transform;
            obstacleTriggered4 = true;
            children[2].SetActive(true);
            ScalePlayer();
            ActivateParticle();
        }

        if (other.CompareTag("Obstacle5") && !obstacleTriggered4)
        {
            childTransform = children[3].transform;
            obstacleTriggered4 = true;
            children[3].SetActive(true);
            ScalePlayer();
            ActivateParticle();
        }

        if (other.CompareTag("Obstacle6") && !obstacleTriggered6)
        {
            obstacleTriggered6 = true;
            children[4].SetActive(true);
            ScalePlayer();
            childTransform = children[4].transform;
            ActivateParticle();
        }

        if (other.CompareTag("Obstacle7") && !obstacleTriggered7)
        {
            childTransform = children[5].transform;
            obstacleTriggered7 = true;
            children[5].SetActive(true);
            ScalePlayer();
            ActivateParticle();
        }

        if (other.CompareTag("Obstacle8") && !obstacleTriggered8)
        {
            childTransform = children[6].transform;
            obstacleTriggered8 = true;
            children[6].SetActive(true);
            ScalePlayer();
            ActivateParticle();
        }

        if (other.CompareTag("hurdle") && !hurdleTrigger)
        {
            objectShaker.ShakeObject(player);
            particles[6].SetActive(true);
            StartCoroutine(SendPlayerBack()); // Start the scaling coroutine

            if (children[2].activeSelf)
            {
                children[2].SetActive(false);
            }
            else if (children[3].activeSelf)
            {
                children[3].SetActive(false);
            }
            playerhealth.TakeDamage(1f); 

            Destroy(hurdle);
        }

        if (other.CompareTag("hurdle_back1"))
        {
            StartCoroutine(SendPlayerBack());
            
            Destroy(hurdle1);
            particles[7].SetActive(true);
            playerhealth.TakeDamage(1f);
        }

        if (other.CompareTag("hurdle_back2"))
        {
            StartCoroutine(SendPlayerBack());
            
            Destroy(hurdle2);
            particles[8].SetActive(true);
            playerhealth.TakeDamage(1f);
        }

        if (other.CompareTag("Jump_Obstacle"))
        {
            DisableYConstraint();
            Jump();


        }
    }

    private System.Collections.IEnumerator ScaleCoroutine()
    {
        float startTime = Time.time;
        float duration = 0.5f;
        Vector3 originalScale = player.transform.localScale;
        Vector3 targetScale = new Vector3(0.9f, 0.9f, 0.9f);

        for (int i = 0; i < 2; i++)
        {
            Vector3 initialScale = player.transform.localScale; // Store the initial scale at the start of each iteration

            while (Time.time < startTime + duration)
            {
                float t = (Time.time - startTime) / duration;
                player.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                yield return null;
            }

            startTime = Time.time;
            player.transform.localScale = originalScale; // Reset the scale to the original value
        }
    }

    private void ScalePlayer()
    {
        player.transform.localScale = Vector3.zero;
        player.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
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

    private void ActivateParticle()
    {
        if (childTransform != null)
        {
            Vector3 adjustedPosition = childTransform.position + new Vector3(0f, 2f, 0f);
            ParticleSystem particleSystem = Instantiate(particlePrefab, adjustedPosition, Quaternion.identity);
            particleSystem.Play();
        }
    }

    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        


    }

    private void DisableYConstraint()
    {
        playerRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        Invoke(nameof(ResetConstraints), jumpDuration);
    }

    private void ResetConstraints()
    {

        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
        playerMovementScript.enabled = false;
        StartCoroutine(RotatePlayer());

    }
    private System.Collections.IEnumerator RotatePlayer()
    {
        float startTime = Time.time;
        float duration = 1.0f; // Adjust the duration of the rotation
        Quaternion startRotation = player.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 90f, 0f);

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            player.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        player.transform.rotation = targetRotation; // Ensure the player reaches the exact target rotation
        firstcamera.SetActive(false);
        secondcamera.SetActive(true);
        shootingscript.enabled = true;
        bulletspawner1.check = true;
        bulletspawner2.check = true;
        bulletspawner3.check = true;

    }
}

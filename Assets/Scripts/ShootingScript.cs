using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet to be instantiated
    public float bulletSpeed = 30f; // Speed of the bullet
    public ParticleSystem particlePrefab;
    public Transform[] targetPositions; // Array of target positions
    public float shootingInterval = 3f; // Interval between shots in seconds
    public GameObject[] boats;
    public GameObject[] particles;
    public CanvasManager canvasmanager; 
   
    private float lastShootTime; // Time when the last shot was fired
    public GameObject player; // Reference to the player GameObject

    private int currentTargetIndex; // Index of the current target position

    private void Start()
    {
        lastShootTime = -shootingInterval; // Initialize the last shoot time to allow the first shot immediately
        currentTargetIndex = 0; // Initialize the current target index to 0
    }

    private void Update()
    {
        if (Time.time - lastShootTime >= shootingInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    private void Shoot()
    {
        // Instantiate the bullet prefab from the player's position
        GameObject bullet = Instantiate(bulletPrefab, player.transform.position, player.transform.rotation);

        // Get the bullet's rigidbody component
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        Instantiate(particlePrefab, bullet.transform.position, bullet.transform.rotation);

        // Get the shooting direction towards the current target position
        Vector3 shootingDirection = GetShootingDirection();

        // Apply velocity to the bullet in the shooting direction
        bulletRigidbody.velocity = shootingDirection * bulletSpeed;
    }

    private Vector3 GetShootingDirection()
    {
        Vector3 shootingDirection = Vector3.zero;

        if (targetPositions.Length > 0)
        {
            // Get the position of the current target
            Vector3 targetPosition = targetPositions[currentTargetIndex].position;
            

            // Calculate the shooting direction towards the target
            shootingDirection = (targetPosition - player.transform.position).normalized;

            // Move to the next target position
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
        }

        return shootingDirection;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bot1"))
        {
            particles[0].SetActive(true);
           
            // Disable the collided game object
            boats[0].SetActive(false); 

            // Activate the particle effect at the collision position
            
            
        }

        if (other.CompareTag("bot2"))
        {
           
            particles[1].SetActive(true);
            // Disable the collided game object
            boats[1].SetActive(false);

            // Activate the particle effect at the collision position
           

        }

        if (other.CompareTag("bot3"))
        {
           
            // Disable the collided game object


            // Activate the particle effect at the collision position
            particles[2].SetActive(true);
            boats[2].SetActive(false);


        }
    }
}

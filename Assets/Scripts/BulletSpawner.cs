using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet to be spawned
    public Transform target; // Target GameObject to spawn bullets towards
    public float minSpawnInterval = 1f; // Minimum time interval between each bullet spawn
    public float maxSpawnInterval = 3f; // Maximum time interval between each bullet spawn
    public float bulletSpeed = 10f; // Speed of the bullets
    public bool check = false;

    private float spawnTimer = 0f; // Timer to track the time between spawns
    private float currentSpawnInterval; // Current time interval between each bullet spawn

    private void Start()
    {
        // Set the initial spawn interval randomly
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void Update()
    {
        if (check)
        {
            // Increment the spawn timer
            spawnTimer += Time.deltaTime;

            // Check if it's time to spawn a bullet
            if (spawnTimer >= currentSpawnInterval)
            {
                // Reset the spawn timer
                spawnTimer = 0f;

                // Spawn a bullet towards the target
                SpawnBullet();

                // Set a new random spawn interval
                currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }

    private void SpawnBullet()
    {
        // Instantiate a new bullet from the prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Calculate the direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Rotate the bullet towards the target
        bullet.transform.rotation = Quaternion.LookRotation(direction);

        // Get the bullet's Rigidbody component
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Set the velocity of the bullet
        bulletRigidbody.velocity = direction * bulletSpeed;
    }
}

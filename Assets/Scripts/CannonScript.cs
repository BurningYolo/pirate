using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;
    public float cannonHeight = 1.4f;
    public float minShootDelay = 7f;
    public float maxShootDelay = 14f;

    private float shootTimer;
    private bool canShoot = true;

    private void Start()
    {
        // Start the shoot timer with a random delay within the specified time window
        shootTimer = Random.Range(minShootDelay, maxShootDelay);
    }

    private void Update()
    {
        // Update the shoot timer
        shootTimer -= Time.deltaTime;

        // Check if enough time has passed to shoot
        if (shootTimer <= 0f && canShoot)
        {
            ShootBullet();

            // Reset the shoot timer with a random delay within the specified time window
            shootTimer = Random.Range(minShootDelay, maxShootDelay);
        }
    }

    public void ShootBullet()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, cannonHeight, transform.position.z - 3f);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        Vector3 targetPosition = new Vector3(transform.position.x, cannonHeight, -11.36f); 
        Vector3 direction = (targetPosition - spawnPosition).normalized;
        bulletRigidbody.velocity = direction * bulletSpeed;
        Destroy(bullet, 6f); 
    }

 
}

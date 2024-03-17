using UnityEngine;
using System.Collections;

public class AIProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform handTransform; // Reference to the character's hand transform
    public float projectileSpeed = 10f;
    public float minShootInterval = 1f;
    public float maxShootInterval = 3f;

    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start shooting coroutine
        StartCoroutine(ShootCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        // Example: AI decides randomly whether to start or stop shooting
        if (Random.value < 0.01f) // Adjust the probability as needed
        {
            ToggleShooting();
        }
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (isShooting)
            {
                ShootProjectile();
            }
            yield return new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));
        }
    }

    void ShootProjectile()
    {
        // Instantiate projectile at character's hand position
        GameObject projectile = Instantiate(projectilePrefab, handTransform.position, Quaternion.identity);

        // Calculate direction towards the direction the character is facing
        Vector3 direction = transform.forward;

        // Set projectile's velocity to shoot in the calculated direction
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        if (projectileRigidbody != null)
        {
            projectileRigidbody.velocity = direction * projectileSpeed;
        }
    }

    void ToggleShooting()
    {
        isShooting = !isShooting;
    }

}
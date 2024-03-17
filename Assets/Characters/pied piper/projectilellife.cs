using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan = 5f; // Lifespan of the projectile in seconds

    private float startTime;

    void Start()
    {
        startTime = Time.time; // Record the start time
    }

    void Update()
    {
        // If the projectile has lived past its lifespan, destroy it
        if (Time.time - startTime >= lifespan)
        {
            Destroy(gameObject);
        }
    }
}

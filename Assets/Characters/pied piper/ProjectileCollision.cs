using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    // Define layers that the projectile can collide with and ignore
    public LayerMask collisionLayer;
    public LayerMask ignoreLayer;

    // Called when the projectile collides with another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider's layer is included in the collision layer mask
        if (((1 << other.gameObject.layer) & collisionLayer) != 0)
        {
            // Handle collision with allowed objects
            Debug.Log("Projectile collided with " + other.gameObject.name);
            
            // Destroy the projectile
            Destroy(gameObject);
        }
        else if (((1 << other.gameObject.layer) & ignoreLayer) != 0)
        {
            // Ignore collision with disallowed objects
            Debug.Log("Projectile ignored collision with " + other.gameObject.name);
        }
    }
}

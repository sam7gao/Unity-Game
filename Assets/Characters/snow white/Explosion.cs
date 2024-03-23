using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    public int explosionDamage = 1;
    public float explosionRadius = 5f;
    public float damageInterval = 1f;
    
    private float lastDamageTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
        lastDamageTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastDamageTime >= damageInterval)
        {
            ApplyDamageToTargets();
            lastDamageTime = Time.time;
        }
    }

    private void ApplyDamageToTargets()
    {
        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to an object with the Health script
            Health healthComponent = collider.GetComponent<Health>();
            if (healthComponent != null)
            {
                // Apply damage to the object
                healthComponent.TakeDamage(explosionDamage);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoe1script : MonoBehaviour
{
    
    private bool collided = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Set collided flag to true upon collision
        collided = true;
        var healthComponent = other.GetComponent<Health>();
        if(healthComponent != null)
        {
        	healthComponent.TakeDamage(3);
        }
    }
}

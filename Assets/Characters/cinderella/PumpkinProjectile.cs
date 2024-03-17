using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinProjectile : MonoBehaviour
{
    public float Speed = 5f;
    public float Lifespan = 3f; // Lifespan of the projectile before it's destroyed
    private SpriteRenderer spriteRenderer;
    private bool movingRight = false; // Store the direction

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(DestroyAfterLifespan());
    }

    private void Update()
    {
        // Move the projectile
        Vector3 movement = transform.right * (movingRight ? 1 : -1) * Speed * Time.deltaTime;
        transform.position += movement;

        // Flip sprite based on movement direction
        if (movement.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    // Method to set the direction of movement
    public void SetDirection(bool moveRight)
    {
        movingRight = moveRight;
    }

    IEnumerator DestroyAfterLifespan()
    {
        yield return new WaitForSeconds(Lifespan);
        Destroy(gameObject);
    }
}

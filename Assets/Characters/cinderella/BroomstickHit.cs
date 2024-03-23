using System.Collections;
using UnityEngine;

public class BroomstickProjectile : MonoBehaviour
{
    public float Speed = 10f;
    public float MaxDistance = 10f; // Maximum distance the broomstick travels
    public float ReturnSpeed = 5f; // Speed at which the broomstick returns
    public float CooldownTime = 2f;
    
    public LayerMask collisionLayer;
    
    private bool isMoving = true; // Flag to check if the broomstick is moving or returning
    private Vector3 spawnPoint; // Store the initial spawn point
    private bool isFacingRight = true; // Flag to check if the broomstick is facing right
	private Coroutine cooldownCoroutine;

    private void Start()
    {
        spawnPoint = transform.position;
        StartCoroutine(ReturnAfterDistance());
    }

    private void Update()
    {
        if (isMoving)
        {
            Move();
        }
        else
        {
            Return();
        }
    }

    private void Move()
    {
        Vector3 movement = transform.right * (isFacingRight ? 1 : -1) * Speed * Time.deltaTime;
        transform.position += movement;
    }

    private void Return()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPoint, ReturnSpeed * Time.deltaTime);
        if (transform.position == spawnPoint)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ReturnAfterDistance()
    {
        yield return new WaitForSeconds(MaxDistance / Speed);
        isMoving = false;
    }

	public void ResetCooldown()
	{
		if (cooldownCoroutine != null)
		{
			StopCoroutine(cooldownCoroutine);
		}
		cooldownCoroutine = StartCoroutine(Cooldown());
	}

    public void SetDirection(bool moveRight)
    {
        isFacingRight = moveRight;
        if (!isFacingRight)
        {
            // Flip the broomstick if it's facing left
            transform.localScale = new Vector3(0.1f, -0.1f, 0.1f);
        }
        else
        {
        	transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
        }
    }
    
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CooldownTime);
        cooldownCoroutine = null;
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with one of the specified layers
        if ((collisionLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            // Handle collision with specified layers
            // You can add your collision handling code here
        }
        
        var healthComponent = other.GetComponent<Health>();
        if(healthComponent != null)
        {
        	healthComponent.TakeDamage(1);
        }
        
    }
}

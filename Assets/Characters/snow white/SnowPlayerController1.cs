using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPlayerController1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator animator;

    public AppleProjectile ProjectilePrefab;
    public PoisonProjectile PoisonPrefab;
    public Transform LaunchOffset;
    public float projectileCooldown = 0.2f;
    public float poisonCooldown = 0.5f;
    private float lastFireTime;
    private float lastFireTime2;

    // Variables for prefab emerging from ground
    public Transform prefabGrowthPoint;
    public GameObject prefabToEmerge;
    public float emergeSpeed = 5f;
    public float remainTime = 2f;
    public float moveDistance = 3f;
    public float coffinCooldown = 5f;
    private float lastEmergeTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastFireTime = -projectileCooldown;
        lastFireTime2 = -poisonCooldown;
        lastEmergeTime = -coffinCooldown;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Detect horizontal movement
        float horizontalMovement = Input.GetAxis("Horizontal1");

        // Set animator parameters based on movement
        animator.SetFloat("xvelocity", Mathf.Abs(horizontalMovement));

        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Move left/right
        float horizontalInput = Input.GetAxis("Horizontal1");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Flip sprite based on movement direction
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.W) && isTouchingGround)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.T) && canAttack())
        {
            FireProjectile();
        }

        if (Input.GetKeyDown(KeyCode.U) && canHit())
        {
            FirePoison();
        }

        // Emerge from ground
        if (Input.GetKeyDown(KeyCode.G) && isTouchingGround && Time.time - lastEmergeTime >= coffinCooldown)
        {
            StartCoroutine(EmergeFromGround());
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    public bool canAttack()
    {
        return IsGrounded() && Time.time - lastFireTime >= projectileCooldown;
    }

	public bool canHit()
	{
		return IsGrounded() && Time.time - lastFireTime2 >= poisonCooldown;
	}
	
    private void FireProjectile()
    {
        lastFireTime = Time.time;
        AppleProjectile projectile = Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        projectile.SetDirection(spriteRenderer.flipX);
    }

    private void FirePoison()
    {
        lastFireTime2 = Time.time;
        PoisonProjectile projectile = Instantiate(PoisonPrefab, LaunchOffset.position, transform.rotation);
        projectile.SetDirection(spriteRenderer.flipX);
    }

	private IEnumerator EmergeFromGround()
	{
    	lastEmergeTime = Time.time;
    	GameObject prefabInstance = Instantiate(prefabToEmerge, prefabGrowthPoint.position, Quaternion.identity);

    	bool facingRight = spriteRenderer.flipX;

    	// Adjust the scale of the prefabInstance based on the player's facing direction
    	if (!facingRight)
    	{
        	Vector3 newScale = prefabInstance.transform.localScale;
        	newScale.x *= -1f;
        	prefabInstance.transform.localScale = newScale;
    	}

    	Vector3 targetPosition = prefabInstance.transform.position + Vector3.up * moveDistance;
    	float elapsedTime = 0f;

    	while (prefabInstance.transform.position != targetPosition)
    	{
        	float step = emergeSpeed * Time.deltaTime;
        	prefabInstance.transform.position = Vector3.MoveTowards(prefabInstance.transform.position, targetPosition, step);
        	elapsedTime += Time.deltaTime;
        	yield return null;
    	}

    	yield return new WaitForSeconds(remainTime);

    	targetPosition = prefabInstance.transform.position - Vector3.up * moveDistance;
    	elapsedTime = 0f;

    	while (prefabInstance.transform.position != targetPosition)
    	{
        	float step = emergeSpeed * Time.deltaTime;
        	prefabInstance.transform.position = Vector3.MoveTowards(prefabInstance.transform.position, targetPosition, step);
        	elapsedTime += Time.deltaTime;
        	yield return null;
    	}

    	Destroy(prefabInstance);
	}

}

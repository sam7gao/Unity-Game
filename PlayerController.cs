using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;


public class PlayerController : MonoBehaviour
{
	
	public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
	
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	private bool isTouchingGround;
	
	public PumpkinProjectile ProjectilePrefab;
    public Transform LaunchOffset;
    public float projectileCooldown = 1.5f;
    private float lastFireTime;
    
	public BroomstickProjectile broomstickPrefab;
    public Transform broomstickSpawnPoint;
    public float broomstickCooldown = 2f;
    private Coroutine broomstickCooldownCoroutine;
    
    public int health = 0;
    [SerializeField] public SpriteRenderer healthbar;
    [SerializeField] public TextMeshProUGUI text;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastFireTime = -projectileCooldown;
        health = 10;
        text.enabled = false;
    }

	private void Update()
    {
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
        
        if(Input.GetKeyDown(KeyCode.G) && canAttack())
		{
    		FireProjectile();
		}
		
		if (Input.GetKeyDown(KeyCode.T) && CanFireBroomstick())
        {
            FireBroomstick();
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

	private void FireProjectile()
	{
		lastFireTime = Time.time;
		PumpkinProjectile projectile = Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
    	projectile.SetDirection(spriteRenderer.flipX);
	}
	
	public bool CanFireBroomstick()
    {
        return IsGrounded() && broomstickCooldownCoroutine == null;
    }

    private void FireBroomstick()
    {
        broomstickCooldownCoroutine = StartCoroutine(BroomstickCooldown());
        BroomstickProjectile broomstick = Instantiate(broomstickPrefab, broomstickSpawnPoint.position, Quaternion.identity);
        broomstick.SetDirection(spriteRenderer.flipX);
        broomstick.ResetCooldown();
    }

    IEnumerator BroomstickCooldown()
    {
        yield return new WaitForSeconds(broomstickCooldown);
        broomstickCooldownCoroutine = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            
            health -= 2;
            healthbar.transform.localScale -= new Vector3(1f, 0f, 0f);


            if (health <= 0)
            {
                Die(); 
            }
        }
        Debug.Log("Cinderella is hit");
    }

    private void Die()
    {
      
        Debug.Log("Your story is over.");
        text.enabled = true;
        Destroy(this);
       
    }

}

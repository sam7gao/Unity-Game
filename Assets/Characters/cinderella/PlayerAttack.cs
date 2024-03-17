using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private float attackCooldown;
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject[] projectiles;
	
	private PlayerController playerController;
	private float cooldownTimer = Mathf.Infinity;
	
    private void Start()
    {
    	playerController = GetComponent<PlayerController>();
    }
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G) && cooldownTimer > attackCooldown && playerController.canAttack())
			Attack();
			
		cooldownTimer += Time.deltaTime;
	}
	
	private void Attack()
	{
		cooldownTimer = 0;
		
		projectiles[0].transform.position = firePoint.position;
		projectiles[0].GetComponent<PumpkinShooter>().SetDirection(Mathf.Sign(transform.localScale.x));
	}

}

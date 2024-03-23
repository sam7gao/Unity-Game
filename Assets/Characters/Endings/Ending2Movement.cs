using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending2Movement : MonoBehaviour
{
    public float speed = 5f; // Speed of character movement

    private Animator animator; // Reference to the animator component

    void Start()
    {
        // Get the Animator component attached to the character
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Move the character leftward
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Check if the character is moving and play the corresponding animation
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}

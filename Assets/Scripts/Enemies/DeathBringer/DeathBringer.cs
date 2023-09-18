using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringer : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float retreatSpeed = 7f;
    public float retreatDistance = 5.0f;


    public DetectionZone meleeAttackZone;
    public DetectionZone domainZone;
    public DetectionZone leftZone;
    public DetectionZone rightZone;

    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private GameObject target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = meleeAttackZone.detectedColliders.Count > 0;

        // Check if the player is detected in the domainZone
        if (domainZone.detectedColliders.Count != 0)
        {
            target = domainZone.detectedColliders[0].gameObject;
        }
        else
        {
            target = null;
        }
    }

    private void FixedUpdate()
    {
        if (target != null && !HasTarget && damageable.IsAlive)
        {
            // Calculate the direction and distance to the target (player)
            Vector2 direction = (target.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);

            // Retreat if the player is too close
            if (distanceToPlayer < retreatDistance)
            {
                // Move the enemy backward
                rb.velocity = new Vector2(-direction.x * retreatSpeed, rb.velocity.y);

                // Play Moving Animation
                animator.SetBool(AnimationStrings.canMove, true);

                // Flip the boss based on the direction
                if (direction.x > 0)
                {
                    FlipBoss(true); // Flip when going right
                }
                else if (direction.x < 0)
                {
                    FlipBoss(false); // No flip when going left
                }
            }
            else
            {
                // Continue chasing the player
                rb.velocity = new Vector2(direction.x * walkSpeed, rb.velocity.y);

                // Play Moving Animation
                animator.SetBool(AnimationStrings.canMove, true);

                // Flip the boss based on the direction
                if (direction.x > 0)
                {
                    FlipBoss(true); // Flip when going right
                }
                else if (direction.x < 0)
                {
                    FlipBoss(false); // No flip when going left
                }
            }
        }
        else
        {
            // If no target, stop moving
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetBool(AnimationStrings.canMove, false);
        }
    }

    private void FlipBoss(bool isFacingLeft)
    {
        // Flip the boss based on the isFacingLeft parameter
        Vector3 newScale = transform.localScale;
        newScale.x = isFacingLeft ? -1.25f : 1.25f; // Flip or unflip
        transform.localScale = newScale;
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
      //  animator.SetBool(AnimationStrings.isHit, true);
    }

}

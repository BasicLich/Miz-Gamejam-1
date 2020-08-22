using System;
using UnityEngine;

public class EnemyController : AbsEnemyController
{

    bool isDead = false;
    public bool isAttacking = false;
    Vector2 velocity, knockbackVelocity;
    public Vector3 target; 
    public int damage = 1; 
    public int health = 3;
    public float attackSpeed = 16.0f;
    public float attackTimer = 0.0f;
    public float windupEnd = 0.35f;
    public float attackEnd = 0.55f;
    public float cooldownEnd = 0.95f;
    public SpriteRenderer spriteRenderer;
    bool hitByPlayer = false;
    private float hitByPlayerAnimTimer = 0f;
    Vector3 knockbackDir;

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerLocation = transform.position;
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (isAttacking) Attack();
        rigidbody2d.velocity = hitByPlayer ? knockbackVelocity : velocity;
        if (hitByPlayer) HitByPlayerAnim();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead || isAttacking || hitByPlayer) return;
        FindPlayer();
        this.target = (lastPlayerLocation - transform.position);
        if (playerSighted)
        {
            if (target.magnitude < 2.0)
            {
                if (!isAttacking) { isAttacking = true; attackTimer = 0.0f; };
            } else
            {
                rigidbody2d.velocity = target.normalized * moveSpeed;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
            }
        }
        else {
            rigidbody2d.velocity = Vector2.zero;
        }
    }
    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public void LoseHealth(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    public void HitByPlayer(Vector3 knockbackDir, int damage)
    {
        if (!hitByPlayer)
        {
            hitByPlayer = true;
            isAttacking = false;
            this.knockbackDir = knockbackDir;
            health -= damage;
            if (health <= 0) Die();
        }
    }
    public void HitByPlayerAnim()
    {
        spriteRenderer.color = Color.red;
        knockbackVelocity = Vector2.Lerp(knockbackDir, Vector2.zero, (float)Math.Sin(hitByPlayerAnimTimer *  Math.PI));
        hitByPlayerAnimTimer += Time.deltaTime;
        if (hitByPlayerAnimTimer > 0.2)
        {
            hitByPlayer = false;
            hitByPlayerAnimTimer = 0;
            spriteRenderer.color = Color.white;
        }
    }

    public void Attack()
    {
        // Windup
        if (attackTimer < windupEnd)
        {
            velocity = Vector2.zero;
            spriteRenderer.color = ((attackTimer % 0.10) < 0.05) ? Color.red : Color.white;
            attackTimer += Time.deltaTime;
        }
        // Attack
        else if (attackTimer < attackEnd)
        {
            spriteRenderer.color = Color.white;
            velocity = target.normalized * attackSpeed;
            attackTimer += Time.deltaTime;
        }
        // Cooldown
        else if (attackTimer < cooldownEnd)
        {
            velocity = Vector2.zero;
            attackTimer += Time.deltaTime;
            spriteRenderer.color = Color.white;
        }
        // End
        else if (isAttacking) isAttacking = false;
    }

    // Attack connected: Skip straight to cooldown
    public void FinishAttack()
    {
        if (attackTimer < attackEnd) attackTimer = attackEnd;
    }
}

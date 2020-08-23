// using System;
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
    public float attackDistance = 2.0f;
    AudioSource alertSound;
    bool seenPlayer = false;
    bool patroling = true;
    static Vector3[] patrolDirections = {Vector3.up, Vector3.right};
    Vector3 patrolDir;

    // Start is called before the first frame update
    void Start()
    {
        patrolDir = patrolDirections[Random.Range(0, 2)];
        lastPlayerLocation = transform.position;
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        alertSound = gameObject.GetComponentInChildren<AudioSource>();
        enemyAngle = Mathf.Atan2(patrolDir.y, patrolDir.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        if (isAttacking) Attack();
        rigidbody2d.velocity = hitByPlayer ? knockbackVelocity : velocity;
        if (hitByPlayer) HitByPlayerAnim();
        if (patroling) Patrol();
    }

    void PlayAlertSound()
    {
        if (!seenPlayer) { alertSound.Play(); seenPlayer = true; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead || isAttacking || hitByPlayer) return;
        FindPlayer();
        this.target = (lastPlayerLocation - transform.position);
        if (playerSighted || seenPlayer)
        {
            patroling = false;
            PlayAlertSound();
            // Player sighted and in range to attack
            if (playerSighted && target.magnitude < attackDistance)
            {
                if (!isAttacking) { isAttacking = true; attackTimer = 0.0f; };
            }
            else if (!playerSighted && target.magnitude < 0.1f)
            {
                seenPlayer = false;
                enemyAngle = Mathf.Atan2(patrolDir.y, patrolDir.x) * Mathf.Rad2Deg;
            }
            // Last position known/player sighted but not in range to attack
            else
            {
                velocity = target.normalized * moveSpeed;
                enemyAngle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            }
        }
        else
        {
            patroling = true;
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
            seenPlayer = true;
            isAttacking = false;
            this.knockbackDir = knockbackDir;
            health -= damage;
            if (health <= 0) Die();
        }
    }
    public void HitByPlayerAnim()
    {
        spriteRenderer.color = Color.red;
        knockbackVelocity = Vector2.Lerp(knockbackDir, Vector2.zero, (float)Mathf.Sin(hitByPlayerAnimTimer * Mathf.PI));
        hitByPlayerAnimTimer += Time.deltaTime;
        if (hitByPlayerAnimTimer > 0.2)
        {
            hitByPlayer = false;
            hitByPlayerAnimTimer = 0;
            spriteRenderer.color = Color.white;
            RealignTowardsPlayer();
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
        else if (isAttacking)
        {
            isAttacking = false;
            RealignTowardsPlayer();
        };
    }

    // Attack connected: Skip straight to cooldown
    public void FinishAttack()
    {
        if (attackTimer < attackEnd) attackTimer = attackEnd;
    }
    void RealignTowardsPlayer()
    {
        this.target = (playerCollider.transform.position - transform.position);
        enemyAngle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
    }

    public void Patrol()
    {
        velocity = patrolDir * moveSpeed/2;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Wall" || collisionInfo.gameObject.tag == "Enemy") patrolDir *= -1;
        enemyAngle = Mathf.Atan2(patrolDir.y, patrolDir.x) * Mathf.Rad2Deg;
    }
}

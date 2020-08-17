using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordController : AbsWeaponController
{

    Vector3 lookingAt;
    public Transform playerTransform;

    public float attackTime = 0.2f; // Number of seconds a melee attack takes to complete

    public float lungeLength = 0.2f;
    public float slashDegrees = 30f;
    private float attackCooldown; // Time between attacks
    private float cooldownTimer = Mathf.Infinity;
    private float animTimer; // 0 to 1, used for lerping animation
    private bool attacking = false;
    private float currentRotation = 0;
    private Vector2 attackTarget;
    private Vector3 startPosition;

    void Start()
    {
       attackCooldown = attackTime;
    }


    public override void Look(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.ReadValue<Vector2>();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = transform.position.z;

        lookingAt = (mouseWorldPos - transform.position).normalized;

        if (!attacking)
        { 
            float zRot = Mathf.Atan2(lookingAt.y, lookingAt.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, zRot);
        }
    }

    public override void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DoMelee();
        }
    }

    public void DoMelee()
    {
        attackTarget = transform.position + lookingAt * lungeLength;
        if (attacking)
        { 
            float zRot = Mathf.Atan2(lookingAt.y, lookingAt.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, zRot);
        }

        startPosition = playerTransform.position;
        cooldownTimer = 0;
        transform.Rotate(0, 0, -slashDegrees/2, Space.Self);
        attacking = true;
    }

    private void Update()
    {
        if (cooldownTimer < attackTime)
        {
            animTimer += Time.deltaTime / attackTime;
            float rotation = slashDegrees * Time.deltaTime / attackTime;
            transform.Rotate(0, 0, rotation, Space.Self);

            transform.position = Vector2.Lerp(playerTransform.position, attackTarget, Mathf.Sin(Mathf.PI * animTimer));

            // If attack is done, reset rotation
            if (cooldownTimer + Time.deltaTime >= attackTime)
            {
                float zRot = Mathf.Atan2(lookingAt.y, lookingAt.x) * Mathf.Rad2Deg;
                transform.position = playerTransform.position;
                transform.rotation = Quaternion.Euler(0, 0, zRot);
                animTimer = 0;
                attacking = false;
            }
        cooldownTimer += Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && attacking)
        {
            Debug.Log("Whack!");
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.Die();
        }
    }

}

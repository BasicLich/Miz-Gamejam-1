using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeWeaponController : AbsWeaponController
{

    Vector3 lookingAt;
    public Transform playerTransform;
    AudioSource hitSound;

    public float attackTime = 0.2f; // Number of seconds a melee attack takes to complete
    public float lungeLength = 0.2f;
    public float slashDegrees = 30f;
    private float attackCooldown; // Time between attacks
    private float cooldownTimer = Mathf.Infinity;
    private float animTimer; // 0 to 1, used for lerping animation
    private bool attacking = false;
    private float currentRotation = 0;
    private Vector3 attackTarget;
    private Vector3 offset;

    void Start()
    {
        offset = transform.localPosition;
        attackCooldown = attackTime;
        foreach (Transform child in transform)
        {
            if (child.tag == "Sound")
            {
                hitSound = child.GetComponent<AudioSource>();
                break;
            }
        }
    }


    public override void Look(InputAction.CallbackContext context)
    {
        if (context.canceled) return;
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
            hitSound.Play();
            DoMelee();
            // GameManager.Instance.transitionToDungeonScene();
        }
    }

    public void DoMelee()
    {
        attackTarget = lookingAt * lungeLength;
        if (attacking)
        { 
            float zRot = Mathf.Atan2(lookingAt.y, lookingAt.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, zRot);
        }

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

            transform.position = Vector2.Lerp(playerTransform.position + offset/2, playerTransform.position + offset/2 + attackTarget, Mathf.Sin(Mathf.PI * animTimer));

            // If attack is done, reset rotation
            if (cooldownTimer + Time.deltaTime >= attackTime)
            {
                float zRot = Mathf.Atan2(lookingAt.y, lookingAt.x) * Mathf.Rad2Deg;
                transform.position = playerTransform.position + offset/2;
                transform.rotation = Quaternion.Euler(0, 0, zRot);
                animTimer = 0;
                attacking = false;
            }
        cooldownTimer += Time.deltaTime;
        }
    }

    // TODO: Only one enemy hit allowed per animation cycle
    void OnTriggerStay2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemies" && attacking)
        {
            EnemyPlayerColliderController enemy = other.gameObject.GetComponent<EnemyPlayerColliderController>();
            enemy.HitByPlayer(lookingAt.normalized * 16, 1);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearController : AbsWeaponController
{
    Vector3 lookingAt;
    public Transform playerTransform;
    AudioSource hitSound;
    public float meleeAttackSpeed; // Number of seconds a melee attack takes to reach apex.

    public float meleeAttackCoolDown;
    float meleeAttackCountdown = Mathf.Infinity;
    public float meleeRange;
    float meleeAttackFraction = 1.0f;

    public Vector3 meleeAttackTarget;
    private Vector3 offset;

    private void Start()
    {
        meleeAttackCoolDown += meleeAttackSpeed * 2;
        offset = transform.localPosition;
        meleeAttackTarget = transform.position;
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
        if (meleeAttackCountdown >= meleeAttackSpeed * 2)
        {
            Vector2 mousePos = context.ReadValue<Vector2>();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            mouseWorldPos.z = transform.position.z;

            lookingAt = (mouseWorldPos - transform.position).normalized;

            float zRot = Mathf.Atan2(lookingAt.y, lookingAt.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, zRot);
        }
    }

    public override void Fire(InputAction.CallbackContext context)
    {
        if (meleeAttackCountdown >= meleeAttackCoolDown && context.performed)
        {
            hitSound.Play();
            DoMelee();
            meleeAttackCountdown = 0.0f;
        }
    }

    public void DoMelee()
    {
        meleeAttackTarget = lookingAt * meleeRange;
        meleeAttackFraction = 0;
    }

    private void Update()
    {
        if (meleeAttackCountdown < meleeAttackCoolDown)
        {
            if (meleeAttackCountdown < meleeAttackSpeed)
            {
                meleeAttackFraction += Time.deltaTime / meleeAttackSpeed;
                transform.position = Vector2.Lerp(playerTransform.position + offset/2, playerTransform.position + offset/2 + meleeAttackTarget, meleeAttackFraction);
                // transform.position = Vector2.Lerp(transform.position, meleeAttackTarget, meleeAttackFraction);
                if (meleeAttackCountdown + Time.deltaTime >= meleeAttackSpeed)
                {
                    meleeAttackFraction = 0;
                }
            }
            else
            {
                meleeAttackFraction += Time.deltaTime / meleeAttackSpeed;
                transform.position = Vector2.Lerp(playerTransform.position + offset/2 + meleeAttackTarget, playerTransform.position + offset/2, meleeAttackFraction);
            }
            meleeAttackCountdown += Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && (meleeAttackCountdown < meleeAttackSpeed * 2))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.Die();
        }
    }
}

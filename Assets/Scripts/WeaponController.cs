using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : AbsWeaponController
{
    Vector3 lookingAt;
    public Transform playerTransform;

    public  float meleeAttackSpeed = 0.2f; // Number of seconds a melee attack takes to reach apex.

    public  float meleeAttackCoolDown = 0.5f;
            float meleeAttackCountdown = 0.5f;
    public float meleeRange = 10.0f;
    float meleeAttackFraction = 1.0f;

    public Vector2 meleeAttackTarget;

    private void Start()
    {
        meleeAttackTarget = transform.position;
    }

    public override void Look(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.ReadValue<Vector2>();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = transform.position.z;

        lookingAt = (mouseWorldPos - transform.position).normalized;

        float zRot = Mathf.Atan2(lookingAt.y, lookingAt.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }

    public override void Fire(InputAction.CallbackContext context)
    {
        if (meleeAttackCountdown >= meleeAttackCoolDown && context.performed)
        {
            DoMelee();
            meleeAttackCountdown = 0.0f;
        }
    }

    public void DoMelee()
    {
        meleeAttackTarget = transform.position + lookingAt * meleeRange;
        meleeAttackFraction = 0;
    }

    private void Update()
    {
        if (meleeAttackCountdown < meleeAttackCoolDown)
        {
            if (meleeAttackCountdown < meleeAttackSpeed)
            {
                meleeAttackFraction += Time.deltaTime / meleeAttackSpeed;
                transform.position = Vector2.Lerp(transform.position, meleeAttackTarget, meleeAttackFraction);
                if (meleeAttackCountdown + Time.deltaTime >= meleeAttackSpeed)
                {
                    meleeAttackFraction = 0;
                }
            }
            else
            {
                meleeAttackFraction += Time.deltaTime / meleeAttackSpeed;
                transform.position = Vector2.Lerp(transform.position, playerTransform.position, meleeAttackFraction);
            }
            meleeAttackCountdown += Time.deltaTime;
        }
    }
}

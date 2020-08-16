using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangedWeaponController : MonoBehaviour
{
    Vector3 lookingAt;
    public Transform playerTransform;

    public  float meleeAttackSpeed = 0.2f; // Number of seconds a melee attack takes to reach apex.

    public  float rangedAttackCoolDown = 0.5f;
    float rangedAttackCountdown = 0.5f;
    public float meleeRange = 10.0f;
    float meleeAttackFraction = 1.0f;

    public Vector2 rangedAttackTarget;

    private void Start()
    {
        rangedAttackTarget = transform.position;
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.ReadValue<Vector2>();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = transform.position.z;

        lookingAt = (mouseWorldPos - transform.position).normalized;

        float zRot = Mathf.Atan2(lookingAt.y, lookingAt.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (rangedAttackCountdown >= rangedAttackCoolDown && context.performed)
        {
            DoRanged();
            rangedAttackCountdown = 0.0f;
        }
    }

    public void DoRanged()
    {
        rangedAttackTarget = transform.position + lookingAt * meleeRange;
        meleeAttackFraction = 0;
    }

    private void Update()
    {
    }
}

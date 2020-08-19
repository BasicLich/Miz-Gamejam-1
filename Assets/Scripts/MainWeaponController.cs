using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class MainWeaponController : MonoBehaviour
{
    public AbsWeaponController equippedWeapon;

    public void Fire(InputAction.CallbackContext context)
    {
        if (equippedWeapon == null) return;
        equippedWeapon.Fire(context);
    }
    public void Look(InputAction.CallbackContext context)
    {
        if (equippedWeapon == null) return;
        equippedWeapon.Look(context);
    }
}

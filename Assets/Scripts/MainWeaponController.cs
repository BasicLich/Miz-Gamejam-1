using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbsWeaponController : MonoBehaviour
{
    public abstract void Look(InputAction.CallbackContext context);
    public abstract void Fire(InputAction.CallbackContext context);
}

public class MainWeaponController : MonoBehaviour
{
    public AbsWeaponController equippedWeapon;

    public void Fire(InputAction.CallbackContext context)
    {
        equippedWeapon.Fire(context);
    }
    public void Look(InputAction.CallbackContext context)
    {
        equippedWeapon.Look(context);
    }
}

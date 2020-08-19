using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbsWeaponController : AbsItem
{
    public abstract void Look(InputAction.CallbackContext context);
    public abstract void Fire(InputAction.CallbackContext context);
}

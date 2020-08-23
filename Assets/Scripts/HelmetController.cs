using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetController : AbsItem
{
    public int extraHealth = 0;

    private void Start()
    {
        if (GameManager.Instance.equippedHelmet != null && GameManager.Instance.equippedHelmet.GetItemId() == GetItemId())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void Equip()
    {
        if (GameManager.Instance != null
            && GameManager.Instance.equippedHelmet != null
            && GameManager.Instance.equippedHelmet.GetItemId() != GetItemId())
        {
            if (GameManager.Instance.equippedHelmet != null) GameManager.Instance.equippedHelmet.Unequip(); 
            GameManager.Instance.maxHealth += extraHealth;
            GameManager.Instance.equippedHelmet = this;
        }
    }

    public void Unequip()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.maxHealth -= extraHealth;
        }
    }
}

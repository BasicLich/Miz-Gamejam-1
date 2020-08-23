using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoController : AbsItem
{
    public int extraHealth = 0;

    private void Start()
    {
        if (GameManager.Instance.equippedTorso != null && GameManager.Instance.equippedTorso.GetItemId() == GetItemId())
        {
            gameObject.SetActive(true);
        } else
        {
            gameObject.SetActive(false);
        }
    }

    public void Equip()
    {
        if (GameManager.Instance != null
            && GameManager.Instance.equippedTorso != null
            && GameManager.Instance.equippedTorso.GetItemId() != GetItemId())
        {
            if (GameManager.Instance.equippedTorso != null) GameManager.Instance.equippedTorso.Unequip(); 
            GameManager.Instance.maxHealth += extraHealth;
            GameManager.Instance.equippedTorso = this;
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

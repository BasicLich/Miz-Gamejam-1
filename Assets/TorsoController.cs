using System;
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
    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.equippedTorso != null && GameManager.Instance.equippedTorso.GetItemId() == GetItemId())
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
            && GameManager.Instance.equippedTorso != null
            && GameManager.Instance.equippedTorso.GetItemId() != GetItemId())
        {
            if (GameManager.Instance.equippedTorso != null) GameManager.Instance.equippedTorso.Unequip(); 
            GameManager.Instance.MaxHealth += extraHealth;
            GameManager.Instance.equippedTorso = this;
        }
    }

    public void Unequip()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.MaxHealth -= extraHealth;
        }
    }

    internal void UpdateLook()
    {
        if (GameManager.Instance.equippedTorso != null && GameManager.Instance.equippedTorso.GetItemId() == GetItemId())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

using System;
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

    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
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
            GameManager.Instance.MaxHealth += extraHealth;
            GameManager.Instance.equippedHelmet = this;
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
        if (GameManager.Instance.equippedHelmet != null && GameManager.Instance.equippedHelmet.GetItemId() == GetItemId())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

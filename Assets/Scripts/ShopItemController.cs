using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemController : MonoBehaviour
{
    public Item item;
    public int cost;


    public void BuyItem()
    {
        if (GameManager.Instance.items.Contains(item.GetItemId()) && GameManager.Instance.Value >= cost)
        {

        }
    }
}

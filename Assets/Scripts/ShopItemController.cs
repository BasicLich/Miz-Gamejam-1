using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public AbsItem item;

    public Text itemText;


    public void BuyItem()
    {
        if (!GameManager.Instance.items.Contains(item.GetItemId()) && GameManager.Instance.Value >= item.cost)
        {
            GameManager.Instance.AddItem(item);
            gameObject.SetActive(false);
        }
    }
}

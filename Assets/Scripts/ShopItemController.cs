using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public AbsItem item;

    public Text itemText;
    public Text costText;

    private void Start()
    {
        if (itemText != null && item != null) itemText.text = item.itemName;
        if (costText != null && item != null) costText.text = item.cost.ToString();
        if (item != null && GameManager.Instance.HasBoughtItem(item)) gameObject.SetActive(false);
    }

    public void BuyItem()
    {
        if (!GameManager.Instance.items.Contains(item.GetItemId()) && GameManager.Instance.Value >= item.cost)
        {
            GameManager.Instance.AddItem(item);
            if (item is ProgressionItem progItem)
            {
                progItem.ActivateItem();
            }
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemController : MonoBehaviour
{
    public AbsItem item;

    // Start is called before the first frame update
    void Start()
    {
        if (item != null) GetComponentInChildren<Text>().text = item.itemName;
        if (!GameManager.Instance.HasBoughtItem(item))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null && !GameManager.Instance.HasBoughtItem(item))
        {
            gameObject.SetActive(false);
        }
    }

    public void EquipItem()
    {
        if (item is TorsoController torso) {
            torso.Equip();
        } else if (item is HelmetController helmet)
        {
            helmet.Equip();
        } else if (item is AbsWeaponController)
        {
            GameManager.Instance.equippedWeapon = item.GetItemId();
        }
    }
}

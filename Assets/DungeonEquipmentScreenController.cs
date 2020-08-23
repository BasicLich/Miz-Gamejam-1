using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonEquipmentScreenController : MonoBehaviour
{
    [SerializeField]
    List<EquipItemController> childControllers;
    public Color equipColor;
    public Color notEquipColor;


    private void OnEnable()
    {
        SetActiveAllChildren(transform, true);
        childControllers = GetComponentsInChildren<EquipItemController>().ToList();
        SetEquippedItem();
    }

    private void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }

    public void SetEquippedItem()
    {
        Debug.Log(equipColor);
        childControllers.ForEach(child => child.SetColor(equipColor, notEquipColor));
    }
}

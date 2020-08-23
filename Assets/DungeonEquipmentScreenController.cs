using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEquipmentScreenController : MonoBehaviour
{
    private void OnEnable()
    {
        SetActiveAllChildren(transform, true);
    }

    private void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }
}

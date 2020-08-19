using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsItem : MonoBehaviour
{

    public int id;
    public int cost;
    public string itemName;
    public int GetItemId() { return id; }
}

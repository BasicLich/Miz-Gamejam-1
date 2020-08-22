using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionItem : AbsItem
{
    public GameObject gameObjectToReplace;

    private void Start()
    {
        if (GameManager.Instance.HasBoughtItem(this))
        {
            ActivateItem();
        } else
        {
            gameObject.SetActive(false);
        }
    }


    public void ActivateItem()
    {
        if (gameObjectToReplace != null)
        {
            gameObjectToReplace.SetActive(false);
        }
        gameObject.SetActive(true);
    }

}

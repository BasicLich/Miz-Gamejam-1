using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterDungeonContoller : MonoBehaviour
{
    public void EnterDungeon()
    {
        GameManager.Instance.transitionToDungeonScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public Text ValueText;
    public int Value { get; set; }

    private void Start()
    {
        if (ValueText != null)
        {
            ValueText.text = Value.ToString();
        } else
        {
            Debug.LogError("GameStateController -- ValueText instance not set");
        }
    }

    public void AddValue(int value)
    {
        Value += value;
        ValueText.text = Value.ToString();
    }
}

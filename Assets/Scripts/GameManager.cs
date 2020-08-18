﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public Text ValueText;

    private int value;
    public int Value { get { return value; } set { this.value = value; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

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

    public void InitializeGUI()
    {
        
    }
}

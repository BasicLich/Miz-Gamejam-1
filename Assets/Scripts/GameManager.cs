using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public Text ValueText;
    public int Value { get; set; }
    public DungeonController dungeonController;

    // dungeonFloors = DungeonGenerator.generateFloors(random parameters, 3)

    public void transitionToDungeonScene()
    {
        dungeonController.generateDungeon(5.7f, 4);
        SceneManager.LoadScene(0);
        // scenetransition to dungeon scene
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            dungeonController = new DungeonController();
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

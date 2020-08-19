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
    public DungeonController dungeonController;

    public List<int> items = new List<int>();

    // dungeonFloors = DungeonGenerator.generateFloors(random parameters, 3)

    public void transitionToDungeonScene()
    {
        dungeonController.generateDungeon(5.7f, 4);
        SceneManager.LoadScene(0);
        // scenetransition to dungeon scene
    }

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
            dungeonController = new DungeonController();
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }


    public void AddValue(int value)
    {
        Value += value;
        if (ValueText == null) return;
        ValueText.text = Value.ToString();
    }

    public void InitializeGUI()
    {
        
    }
}

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
    public bool debug = false;


    // --- Player Stats ---
    public int maxHealth = 3;

    public HelmetController equippedHelmet;

    public TorsoController equippedTorso;
    
    public int equippedWeapon;
    
    // --- END Plyer Stats ---

    // dungeonFloors = DungeonGenerator.generateFloors(random parameters, 3)

    public void transitionToDungeonScene()
    {
        dungeonController.generateDungeon(5.7f, 4);
        SceneManager.LoadScene(0);
    }
    public void transitionToCampScene()
    {
        SceneManager.LoadScene(2);
    }

    
    public int value;
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
            if (debug) value = 10000;
        }
    }


    public void AddValue(int value)
    {
        Value += value;
        if (ValueText == null) return;
        ValueText.text = Value.ToString();
    }

    public void AddItem(AbsItem item)
    {
        if (items.Contains(item.GetItemId())) return;
        items.Add(item.GetItemId());
        Value -= item.cost;
        if (ValueText == null) return;
        ValueText.text = Value.ToString();
    }

    public bool HasBoughtItem(AbsItem item)
    {
        return Instance.items.Contains(item.GetItemId());
    }

    public void InitializeGUI()
    {
        
    }
}

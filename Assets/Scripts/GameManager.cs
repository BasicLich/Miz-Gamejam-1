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
    public Text GiftedText;

    public DungeonController dungeonController;
    public int amountToGive = 50;
    public int amountGiven = 0;

    public List<int> items = new List<int>();

    // dungeonFloors = DungeonGenerator.generateFloors(random parameters, 3)

    public void transitionToDungeonScene()
    {
        dungeonController.generateDungeon(5.7f, 4);
        SceneManager.LoadScene(0);
        // scenetransition to dungeon scene
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

    public void GiveMoneyToThePoor()
    {
        if (value > amountToGive)
        {
            value -= amountToGive;
            amountGiven += amountToGive;
            if (GiftedText != null)
            {
                GiftedText.text = amountGiven.ToString();
            }
        } else
        {
            amountGiven += value;
            if (GiftedText != null)
            {
                GiftedText.text = amountGiven.ToString();
            }
            value = 0;
        }
    }

    public bool HasBoughtItem(AbsItem item)
    {
        return Instance.items.Contains(item.GetItemId());
    }

    public void InitializeGUI()
    {
        
    }
}

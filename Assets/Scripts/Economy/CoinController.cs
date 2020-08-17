using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public GameStateController gameState;
    public int value = 5;
    public Text valueText;

    public List<Sprite> lowValueTextures = new List<Sprite>();
    public List<Sprite> middleValueTextures = new List<Sprite>();
    public List<Sprite> highValueTextures = new List<Sprite>();
    public List<Sprite> highestValueTextures = new List<Sprite>();

    private void Start()
    {
        if (gameState == null)
        {
            gameState = Component.FindObjectOfType<GameStateController>();
        }
        SetSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Coin Pickup!");
            gameState.AddValue(value);
            Destroy(gameObject);
        }
    }

    public void SetSprite()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Sprite chosenSprite;
        if (value < 50)
        {
           chosenSprite  = lowValueTextures[Random.Range(0, lowValueTextures.Count)];
        } else if (value < 100)
        {
            chosenSprite = middleValueTextures[Random.Range(0, middleValueTextures.Count)];
        } else if (value < 200)
        {
            chosenSprite = highValueTextures[Random.Range(0, highValueTextures.Count)];
        } else
        {
            chosenSprite = highestValueTextures[Random.Range(0, highestValueTextures.Count)];
        }
        spriteRenderer.sprite = chosenSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public int value = 5;
    public int[] possibleValues = {5, 5, 5, 5, 10, 10, 20, 20, 50, 100};
    public Text valueText;

    public List<Sprite> lowValueTextures = new List<Sprite>();
    public List<Sprite> middleValueTextures = new List<Sprite>();
    public List<Sprite> highValueTextures = new List<Sprite>();
    public List<Sprite> highestValueTextures = new List<Sprite>();

    private void Start()
    {
        value = possibleValues[Random.Range(0, possibleValues.Length)];
        SetSprite();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
        GameManager.Instance.AddValue(value);
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

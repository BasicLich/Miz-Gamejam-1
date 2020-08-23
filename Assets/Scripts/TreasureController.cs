using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite closedSprite;
    public Sprite openSprite;
    SpriteRenderer spriteRenderer;
    int value;
    bool opened = false;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedSprite;
    }

    public void SetValue(int value)
    {
        this.value = value;
    }
    public void OpenTreasure()
    {
        if (!opened)
        {
            GameManager.Instance.AddValue(value);
            spriteRenderer.sprite = openSprite;
            opened = true;
        }
    }
}

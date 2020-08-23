using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    public int heartsPerRow = 6;
    List<Image> hearts = new List<Image>();

    public Image heartImage;

    PlayerController player;

    private void Start()
    {
        hearts = new List<Image>();
        player = FindObjectOfType<PlayerController>();
        CreateHearts();
    }

    public void CreateHearts()
    {
        if (hearts != null && hearts.Count > 0)
        {
            hearts.ForEach(heart => Destroy(heart.gameObject));
        }

        for (int i = 0; i < GameManager.Instance.maxHealth; i++)
        {
            var heart = Instantiate(heartImage, transform);
            hearts.Add(heart);
            heart.rectTransform.localPosition = new Vector2(25 + 30 * (i % heartsPerRow), 5 - 25 * (i / heartsPerRow));
            //Debug.Log($"{25 * (i % heartsPerRow)} , {25 * (i / heartsPerRow)}");
        }
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i > player.health - 1)
            {
                hearts[i].gameObject.SetActive(false);
            } else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}

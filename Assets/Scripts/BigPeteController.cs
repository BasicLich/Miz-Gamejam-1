using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigPeteController : MonoBehaviour
{
    public Text bigPeteText;
    public Canvas bigPeteCanvas;
    public Button bigPeteShopBtn;

    // Start is called before the first frame update
    void Start()
    {
        if (bigPeteText == null)
        {
            bigPeteText = GetComponentInChildren<Text>();
        }
        if (bigPeteCanvas == null)
        {
            bigPeteCanvas = GetComponentInChildren<Canvas>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HELLO STEALEN! HOW ARE YOU?!");
        bigPeteText.gameObject.SetActive(true);
        bigPeteShopBtn.gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        bigPeteText.gameObject.SetActive(false);
        bigPeteShopBtn.gameObject.SetActive(false);
    }
}

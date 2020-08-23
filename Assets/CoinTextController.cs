using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTextController : MonoBehaviour
{
    public Text textElement;
    // Start is called before the first frame update
    void Start()
    {
        textElement.text = GameManager.Instance.Value.ToString();
    }
}

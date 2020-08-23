using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Canvas txt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        txt.gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        txt.gameObject.SetActive(false);
    }
}

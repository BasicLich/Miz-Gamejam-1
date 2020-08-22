using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public Transform parentTransform;
    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, -parentTransform.eulerAngles.z);
        //Debug.Log("pepepepepep");
    }
}

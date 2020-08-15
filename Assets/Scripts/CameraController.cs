using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;

    public float cameraBoxWidth = 16.0f;
    public float cameraBoxHeight = 9.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        var position = transform.position;

        if (playerTransform.position.x < position.x - cameraBoxWidth)
        {

        }
        else if (playerTransform.position.x > position.x + cameraBoxWidth)
        {
            
        }
    }
}

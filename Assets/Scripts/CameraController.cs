using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public Transform playerTransform;

    public PlayerController player;

    bool chasingPlayer = false;
    public float chaseSpeedOffset = 1.0f; // How much faster or slower is the camera?
    public float accelerationTime = 1.0f; // How many seconds does it take for the camera to reach max speed?
    public float decelerationTime = 0.5f; // How many seconds does it take for the camera to slow down to a crawl?
    
    float acceleration = 0.0f;
    float deceleration = 0.0f;
    Vector3 velocity = new Vector3();


    public float cameraBoxWidth = 16.0f;
    public float cameraBoxHeight = 9.0f;

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Rect getRekt = GameManager.Instance.dungeonController.getActiveRoomRect();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        //transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        // var position = transform.position;

        //if (player.transform.position.x < position.x - cameraBoxWidth && !chasingPlayer)
        //{
        //    position.x = player.transform.position.x + cameraBoxWidth;
        //}
        //else if (player.transform.position.x > position.x + cameraBoxWidth && !chasingPlayer)
        //{
        //    position.x = player.transform.position.x - cameraBoxWidth;
        //}

        //if (player.transform.position.y < position.y - cameraBoxHeight && !chasingPlayer)
        //{
        //    position.y = player.transform.position.y + cameraBoxHeight;
        //}
        //else if (player.transform.position.y > position.y + cameraBoxHeight && !chasingPlayer)
        //{
        //    position.y = player.transform.position.y - cameraBoxHeight;
        //}
        // if (isPlayerOutsideCameraBox() && !chasingPlayer)
        // {
        //     StartChase();
        // }

        //transform.position = position;
        // if (chasingPlayer)
        // {
        //     Vector2 distanceVector = player.transform.position - transform.position;

        //     if (acceleration < 1.0f)
        //     {
        //         acceleration += Time.deltaTime / accelerationTime;
        //     }
        //     if (distanceVector.magnitude < 2.0f)
        //     {
        //         if (deceleration >= 1.0f)
        //         {
        //             chasingPlayer = false;
        //             velocity = new Vector3();
        //         } else
        //         {
        //             deceleration += Time.deltaTime / decelerationTime;
        //             velocity = distanceVector.normalized * Mathf.Lerp(player.moveSpeed + chaseSpeedOffset, 0.0f, deceleration);
        //         }
        //     }
        //     else
        //     {
        //         transform.position = position + velocity * Time.deltaTime;
        //         velocity = distanceVector.normalized * Mathf.Lerp(0.0f, player.moveSpeed + chaseSpeedOffset, acceleration);
        //         velocity.z = 0.0f;
        //     }
        //     transform.position = position + velocity * Time.deltaTime;
        // }
    }

    void StartChase()
    {
        chasingPlayer = true;
        acceleration = 0.0f;
        deceleration = 0.0f;
    }

    bool isPlayerOutsideCameraBox()
    {
        var position = transform.position;
        return player.transform.position.x < position.x - cameraBoxWidth
            || player.transform.position.x > position.x + cameraBoxWidth
            || player.transform.position.y < position.y - cameraBoxHeight
            || player.transform.position.y > position.y + cameraBoxHeight;
    }
}

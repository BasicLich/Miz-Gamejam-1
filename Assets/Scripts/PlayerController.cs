using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * moveSpeed * Time.deltaTime;
    }

    public void SetMovement(InputAction.CallbackContext context)
    {
        Debug.Log("Test");
        Debug.Log(context.ReadValue<Vector2>());
    }
}

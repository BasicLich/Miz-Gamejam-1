using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    Vector2 velocity;
    public Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity = velocity;
    }

    public void SetMovement(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>() * moveSpeed;
    }
}

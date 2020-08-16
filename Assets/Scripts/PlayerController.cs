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
    private double animTimer = 0;
    public double animSpeed = 0.3;
    public double animBobHeight = 0.2;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity = velocity;
        moveAnim();
    }

    public void SetMovement(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>() * moveSpeed;
    }

    private bool isMoving()
    {
        return (velocity.x != 0 || velocity.y != 0);
    }

    private void moveAnim() 
    {
        if (isMoving())
        {
        animTimer += animSpeed;
        transform.localScale = new Vector3(0.7F, (float)(0.7 - animBobHeight + Math.Abs(animBobHeight * Math.Sin(animTimer/10))), 0.7F);
        }
    }
}

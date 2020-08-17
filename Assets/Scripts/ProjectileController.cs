using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody2d;
    public BoxCollider2D collider2d;
    void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        collider2d = gameObject.GetComponent<BoxCollider2D>();
        rigidbody2d.velocity = transform.right * speed;
    }
    void Update()
    {
        // transform.position += transform.right * Time.deltaTime * speed;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        float selfDestructTimer = 5.0f;
        rigidbody2d.velocity = Vector3.zero;
        Destroy(rigidbody2d);
        Destroy(collider2d);
        if (collisionInfo.gameObject.tag == "Enemy") {
            selfDestructTimer = 0.0f;
            EnemyController enemy = collisionInfo.gameObject.GetComponent<EnemyController>();
            enemy.Die();
        };
        Destroy(gameObject, selfDestructTimer);
    }
}


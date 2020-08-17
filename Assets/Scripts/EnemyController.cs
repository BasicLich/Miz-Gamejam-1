using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float fieldOfView = 70.0f;
    public float visionRange = 20.0f;
    public int visionFidelity = 10;
    public BoxCollider2D playerCollider;
    public float moveSpeed = 4.0f;

    Vector3 lastPlayerLocation;

    public Vector2 lookDir = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lookDir = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * new Vector2(1, 0);
        Vector2 left = (Quaternion.Euler(0, 0, fieldOfView / 2.0f) * lookDir);
        Vector2 right = (Quaternion.Euler(0, 0, -fieldOfView / 2.0f) * lookDir);

        float stepX = (left.x - right.x) / visionFidelity;
        float stepY = (left.y - right.y) / visionFidelity;

        Vector2 temp = right;

        Vector2 position = transform.position;
        bool playerSighted = false;
        
        for (int i = 0; i < visionFidelity; i++)
        {

            RaycastHit2D hit = Physics2D.Raycast(position, temp, visionRange);
            Debug.DrawLine(transform.position, hit.point);

            if (hit.collider == playerCollider)
            {
                playerSighted = true;
                lastPlayerLocation = playerCollider.transform.position;
                break;
            }

            temp.x += stepX;
            temp.y += stepY;
        }
        if (playerSighted)
        {
            Vector3 target = (lastPlayerLocation - transform.position).normalized;
            transform.position += target * moveSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //return;
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(lookDir.x, lookDir.y).normalized * visionRange);
        Vector2 left = (Quaternion.Euler(0, 0, fieldOfView / 2.0f) * lookDir); 
        Vector2 right = (Quaternion.Euler(0, 0, -fieldOfView / 2.0f) * lookDir);
        Debug.DrawRay(transform.position, new Vector3(left.x, left.y, 0).normalized * visionRange);
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(left.x, left.y).normalized * visionRange);
        Debug.DrawRay(transform.position, new Vector3(right.x, right.y, 0).normalized * visionRange);

        float stepX = (left.x - right.x) / visionFidelity;
        float stepY = (left.y - right.y) / visionFidelity;

        Vector2 temp = right;

        for (int i = 0; i < visionFidelity; i++)
        {
            Debug.DrawRay(transform.position, (new Vector3(temp.x, temp.y, 0).normalized * visionRange), Color.red);
            temp.x += stepX;
            temp.y += stepY;
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "AttackingMeleeWeapon") Die();
    // }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Projectile") Die();
    }

    public void Die() {
        Destroy(gameObject);
    }
}

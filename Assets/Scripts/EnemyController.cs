using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float fieldOfView = 70.0f;
    public float visionRange = 20.0f;
    public int visionFidelity = 10;
    public BoxCollider2D playerCollider;
    bool playerSighted = false;

    public float moveSpeed = 4.0f;

    public bool debug = false;

    public float FoVRad { get{ return fieldOfView * Mathf.Deg2Rad; }}

    Vector3 lastPlayerLocation;

    public Vector2 lookDir = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerLocation = transform.position;
    }

    void FindPlayer()
    {
        playerSighted = false;
        lookDir = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * new Vector2(1, 0);
        Vector2 left = (Quaternion.Euler(0, 0, fieldOfView / 2.0f) * lookDir);
        Vector2 right = (Quaternion.Euler(0, 0, -fieldOfView / 2.0f) * lookDir);

        //float stepX = (left.x - right.x) / visionFidelity;
        //float stepY = (left.y - right.y) / visionFidelity;

        Vector3 temp;

        Vector2 position = transform.position;
        float radOffset = Mathf.Atan2(right.y, right.x);

        for (int i = 0; i < visionFidelity; i++)
        {

            float dir = Random.Range(0, 1.0f) * FoVRad + radOffset;
            temp = new Vector3(visionRange * Mathf.Cos(dir), visionRange * Mathf.Sin(dir), 0);
            RaycastHit2D hit = Physics2D.Raycast(position, temp.normalized, visionRange);
            //Debug.Log(temp);
            if (debug) Debug.DrawRay(transform.position, temp.normalized * visionRange, Color.blue);

            if (hit.collider == playerCollider)
            {
                playerSighted = true;
                lastPlayerLocation = playerCollider.transform.position;
                break;
            }

            //temp.x += stepX;
            //temp.y += stepY;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindPlayer();
        Vector3 target = (lastPlayerLocation - transform.position);
        if (playerSighted || target.magnitude > 0.1f)
        {
            transform.position += target.normalized * moveSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
        }
    }

    private void OnDrawGizmos()
    {
        return;
        Gizmos.color = Color.red;
        //return;
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(lookDir.x, lookDir.y).normalized * visionRange);
        Vector2 left = (Quaternion.Euler(0, 0, fieldOfView / 2.0f) * lookDir); 
        Vector2 right = (Quaternion.Euler(0, 0, -fieldOfView / 2.0f) * lookDir);
        Gizmos.DrawRay(transform.position, new Vector3(left.x, left.y, 0).normalized * visionRange);
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(left.x, left.y).normalized * visionRange);
        Gizmos.DrawRay(transform.position, new Vector3(right.x, right.y, 0).normalized * visionRange);

        float radOffset = Mathf.Atan2(right.y, right.x);
        
        Vector2 temp = right;

        for (int i = 0; i < visionFidelity; i++)
        {
            float dir = Random.Range(0, 1.0f) * FoVRad + radOffset;
            temp = new Vector2(Mathf.Cos(dir), Mathf.Sin(dir));
            Gizmos.DrawRay(transform.position, (new Vector3(temp.x, temp.y, 0).normalized * visionRange));
            //temp.x += stepX;
            //temp.y += stepY;
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "AttackingMeleeWeapon") Die();
    // }
    public void Die() {
        Destroy(gameObject);
    }
}

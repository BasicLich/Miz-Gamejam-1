using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsEnemyController : MonoBehaviour
{
    public float fieldOfView = 70.0f;
    public float visionRange = 20.0f;
    public int visionFidelity = 10;
    public BoxCollider2D playerCollider;
    protected bool playerSighted = false;
    public float FoVRad { get { return fieldOfView * Mathf.Deg2Rad; } }
    protected Vector3 lastPlayerLocation;
    protected Vector2 lookDir = new Vector2(1, 0);

    public bool debug = false;
    public float moveSpeed = 4.0f;

    protected void Start()
    {
        playerCollider = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
    }

    protected void FindPlayer()
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

    public void Die()
    {
        Destroy(gameObject);
    }
}

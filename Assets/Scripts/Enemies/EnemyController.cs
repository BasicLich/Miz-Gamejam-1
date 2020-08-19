using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : AbsEnemyController
{
    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        lastPlayerLocation = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindPlayer();
        target = (lastPlayerLocation - transform.position);
        if (playerSighted || target.magnitude > 0.1f)
        {
            // transform.position += target.normalized * moveSpeed * Time.deltaTime;
            rigidbody2d.velocity = target.normalized * moveSpeed;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
        } else
        {
            rigidbody2d.velocity = Vector2.zero;
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    //private void OnDrawGizmos()
    //{
    //    return;
    //    Gizmos.color = Color.red;
    //    //return;
    //    //Gizmos.DrawLine(transform.position, transform.position + new Vector3(lookDir.x, lookDir.y).normalized * visionRange);
    //    Vector2 left = (Quaternion.Euler(0, 0, fieldOfView / 2.0f) * lookDir); 
    //    Vector2 right = (Quaternion.Euler(0, 0, -fieldOfView / 2.0f) * lookDir);
    //    Gizmos.DrawRay(transform.position, new Vector3(left.x, left.y, 0).normalized * visionRange);
    //    //Gizmos.DrawLine(transform.position, transform.position + new Vector3(left.x, left.y).normalized * visionRange);
    //    Gizmos.DrawRay(transform.position, new Vector3(right.x, right.y, 0).normalized * visionRange);

    //    float radOffset = Mathf.Atan2(right.y, right.x);

    //    Vector2 temp = right;

    //    for (int i = 0; i < visionFidelity; i++)
    //    {
    //        float dir = Random.Range(0, 1.0f) * FoVRad + radOffset;
    //        temp = new Vector2(Mathf.Cos(dir), Mathf.Sin(dir));
    //        Gizmos.DrawRay(transform.position, (new Vector3(temp.x, temp.y, 0).normalized * visionRange));
    //        //temp.x += stepX;
    //        //temp.y += stepY;
    //    }
    //}
}

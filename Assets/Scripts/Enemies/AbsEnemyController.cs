using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsEnemyController : MonoBehaviour
{
    public float fieldOfView = 70.0f;
    public float visionRange = 20.0f;
    public int visionFidelity = 10;
    public BoxCollider2D playerCollider;
    public Rigidbody2D rigidbody2d;
    [SerializeField]
    protected bool playerSighted = false;
    public float FoVRad { get { return fieldOfView * Mathf.Deg2Rad; } }

    [SerializeField]
    protected Vector3 lastPlayerLocation;
    protected Vector2 lookDir = new Vector2(1, 0);
    public float angle;
    public bool debug = false;
    public float moveSpeed = 4.0f;
    private Mesh mesh;

    void Awake()
    {
        playerCollider = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        mesh = new Mesh();
        MeshFilter filter = GetComponentInChildren<MeshFilter>();
        if (filter != null) filter.mesh = mesh;
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
        float degOffset = Mathf.Atan2(right.y, right.x) * Mathf.Rad2Deg;
        angle = transform.rotation.eulerAngles.z + fieldOfView/2.0f;
        float angleIncrease = fieldOfView / visionFidelity;

        Vector3[] vertices = new Vector3[visionFidelity + 2];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[visionFidelity * 3];
        int vertexIndex = 1;
        int triIndex = 0;
        for (int i = 0; i < visionFidelity; i++)
        {
            Vector3 vertex;

            //float dir = Random.Range(0, 1.0f) * FoVRad + radOffset;
            Vector3 dir = GetVectorFromAngle(angle).normalized;

            temp = dir * visionRange;// new Vector3(visionRange * Mathf.Cos(dir), visionRange * Mathf.Sin(dir), 0);
            RaycastHit2D hit = Physics2D.Raycast(position, temp.normalized, visionRange);
            //Debug.Log(temp);
            Vector3 pointHit = new Vector3(hit.point.x, hit.point.y, transform.position.z);
            Debug.Log(pointHit);
            if (debug) Debug.DrawRay(position,temp.normalized * visionRange, Color.gray);

            if (hit.collider == playerCollider)
            {
                playerSighted = true;
                lastPlayerLocation = playerCollider.transform.position;
                vertex = pointHit + new Vector3(temp.x, temp.y);// - new Vector2(position.x, position.y);
                if (debug) Debug.DrawRay(position, pointHit, Color.red);
                //vertex = Vector3.zero + temp;
                //break;
            }
            else if (hit.collider == null)
            {
                vertex = Vector3.zero + temp;
                if (debug) Debug.DrawRay(position, temp, Color.blue);
            }
            else
            {
                vertex = pointHit + new Vector3(temp.x, temp.y);// - new Vector2(position.x, position.y);
                if (debug) Debug.DrawRay(position, pointHit, Color.white);
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triIndex] = 0;
                triangles[triIndex + 1] = vertexIndex - 1;
                triangles[triIndex + 2] = vertexIndex;
                triIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;

            //temp.x += stepX;
            //temp.y += stepY;
        }
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
    }

    private void OnDrawGizmos()
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
        float degOffset = Mathf.Atan2(right.y, right.x) * Mathf.Rad2Deg;
        angle = transform.rotation.eulerAngles.z + fieldOfView / 2.0f;
        float angleIncrease = fieldOfView / visionFidelity;

        Vector3[] vertices = new Vector3[visionFidelity + 2];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[visionFidelity * 3];
        int vertexIndex = 1;
        int triIndex = 0;
        for (int i = 0; i < visionFidelity; i++)
        {
            Vector3 vertex;

            //float dir = Random.Range(0, 1.0f) * FoVRad + radOffset;
            Vector3 dir = GetVectorFromAngle(angle).normalized;

            temp = dir * visionRange;// new Vector3(visionRange * Mathf.Cos(dir), visionRange * Mathf.Sin(dir), 0);
            RaycastHit2D hit = Physics2D.Raycast(position, temp.normalized, visionRange);
            //Debug.Log(temp);
            Vector3 pointHit = new Vector3(hit.point.x, hit.point.y, transform.position.z);
            Debug.Log(pointHit);
            if (debug) Debug.DrawRay(position, temp.normalized * visionRange, Color.gray);
            if (pointHit != Vector3.zero) Gizmos.DrawSphere(pointHit, 0.1f);
            if (hit.collider == playerCollider)
            {
                playerSighted = true;
                lastPlayerLocation = playerCollider.transform.position;
                vertex = pointHit + new Vector3(temp.x, temp.y);// - new Vector2(position.x, position.y);
                if (debug) Debug.DrawRay(position, pointHit, Color.red);
                //vertex = Vector3.zero + temp;
                //break;
            }
            else if (hit.collider == null)
            {
                vertex = Vector3.zero + temp;
                if (debug) Debug.DrawRay(position, temp, Color.blue);
            }
            else
            {
                vertex = pointHit + new Vector3(temp.x, temp.y);// - new Vector2(position.x, position.y);
                if (debug) Debug.DrawRay(position, pointHit, Color.white);
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triIndex] = 0;
                triangles[triIndex + 1] = vertexIndex - 1;
                triangles[triIndex + 2] = vertexIndex;
                triIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;

            //temp.x += stepX;
            //temp.y += stepY;
        }
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        //mesh.vertices = vertices;
        //mesh.uv = uvs;
        //mesh.triangles = triangles;
    }
}

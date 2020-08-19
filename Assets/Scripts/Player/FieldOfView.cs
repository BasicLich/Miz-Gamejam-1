using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float fov = 360.0f;
    public int rayCount = 10;
    public float viewDistance = 20.0f;

    //private MeshFilter filter;
    private Mesh mesh;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        Vector3 origin = transform.position;
        float angle = 0.0f;
        float angleIncrease = fov / rayCount;

        vertices[0] = Vector3.zero;
        //vertices[1] = new Vector3(50, 0);
        //vertices[2] = new Vector3(0, -50);

        int vertexIndex = 1;
        int triIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            Vector3 dir = GetVectorFromAngle(angle).normalized;


            var hit = Physics2D.Raycast(origin, dir, viewDistance, LayerMask.GetMask("Default"));

            if (hit.collider == null)
            {
                vertex = Vector3.zero + dir * viewDistance;
                Debug.DrawLine(origin, vertex);
            }
            else
            {
                vertex = hit.point + new Vector2(dir.x, dir.y) - new Vector2(origin.x, origin.y);
                Debug.DrawLine(origin, hit.point);
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

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}

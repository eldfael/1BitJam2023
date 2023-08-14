using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCastTest : MonoBehaviour
{

    Mesh mesh;
    MeshRenderer meshRenderer;

    public float _radius = 180f;
    public int _rayCount = 100;
    public float _dist = 10f;
    public float _angle = 0f;
    //public Vector2 position = new Vector2(0,0);


    float radius;
    Vector3 origin;
    int rayCount;
    float dist;
    float angle;
    float angleIncrease;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(235/255f, 127/255f, 120/255f, 1f));
        
        radius = _radius;
        rayCount = _rayCount;
        angleIncrease = radius / rayCount;
        
        dist = _dist;

    }

    private void Update()
    {
        //radius = _radius;
        //rayCount = _rayCount;
        //dist = _dist;
        
        //angleIncrease = radius / rayCount;
        angle = _angle;
        origin = transform.position;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];


        //unnsure ?
        vertices[0] = Vector3.zero;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), dist);
            Debug.DrawRay(origin, GetVectorFromAngle(angle),Color.red, dist);
            if (raycastHit.collider == null)
            {
                vertex = origin - transform.position + GetVectorFromAngle(angle) * dist;
                //vertex = GetVectorFromAngle(angle) * dist; ignore or whatever lol - will fix "elegantly" later
            }
            else
            {
                /*Vector2 v = new Vector2(
                    raycastHit.point.x + transform.position.x, 
                    raycastHit.point.y + transform.position.y
                    );
                
                
                vertex = v;
                */
                vertex = raycastHit.point - new Vector2(transform.position.x,transform.position.y);
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }



    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}

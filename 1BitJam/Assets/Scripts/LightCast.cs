using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCast : MonoBehaviour
{
    Mesh mesh;
    LayerMask lmask;

    static float DEFAULTRADIUS = 90f;
    static int DEFAULTRAYS = 32;
    static float DEFAULTDISTANCE = 10f;
    static float DEFAULTANGLE = -45f;

    public float radius = DEFAULTRADIUS;
    public int rays = DEFAULTRAYS;
    public float distance = DEFAULTDISTANCE;
    public float angle = DEFAULTANGLE;

    bool fire = false;

    /*
    float radius;
    int rays;
    float distance;
    float angle;
    */
    
    float currentAngle;
    float angleIncrease;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        lmask = LayerMask.GetMask("Default") + LayerMask.GetMask("AxeBox");

        
        if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.gameObject.GetComponent<Flammable>() != null)
        {
            fire = true;
        }

        angleIncrease = radius / rays;
    }

    private void Update()
    {
        currentAngle = angle;

        Vector3[] vertices = new Vector3[rays + 2];
        int[] triangles = new int[rays * 3];

        vertices[0] = Vector3.zero;
        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rays; i++)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, GetVectorFromAngle(currentAngle), distance, lmask);

            if (raycastHit.collider == null || raycastHit.collider.tag == "Player")
            {
                //If raycast has hit nothing - draw vertex at max distance
                vertices[vertexIndex] = GetVectorFromAngle(currentAngle) * distance;
                if (raycastHit.collider != null)
                {
                    raycastHit.collider.gameObject.GetComponent<PlayerController>().PlayerDeath();
                    raycastHit = Physics2D.Raycast(transform.position, GetVectorFromAngle(currentAngle), distance, lmask);
                    if (raycastHit.collider != null)
                    {
                        vertices[vertexIndex] = raycastHit.point - new Vector2(transform.position.x, transform.position.y);
                    }
                }

            }
            else
            {
                //If raycast has hit collidable object - draw vertex at point of collision
                vertices[vertexIndex] = raycastHit.point - new Vector2 (transform.position.x, transform.position.y);
                if (raycastHit.collider.tag == "LightDetector")
                {
                    raycastHit.collider.gameObject.GetComponent<LightDetectorController>().SetState(true);
                }
                //spark fire
                if (fire == true && raycastHit.collider.gameObject.GetComponent<Flammable>() != null)
                {
                    Debug.Log("Spread Fire");
                    raycastHit.collider.gameObject.GetComponent<Flammable>().CatchFire();
                }
            }
            
            if (i > 0)
            {
                //Setting orientation of triangle with new point that was just created
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            currentAngle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

    }
    private static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public bool CheckLight(GameObject obj)
    {
        float cur = 0;
        for (int i = 0; i < rays; i++)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, GetVectorFromAngle(cur), distance, lmask);
            if (raycastHit.collider != null && raycastHit.collider.gameObject == obj)
            {
                return true;            
            }
            cur -= angleIncrease;

        }
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLight : MonoBehaviour
{
    LayerMask lmask;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        lmask = LayerMask.GetMask("Default") + LayerMask.GetMask("AxeBox");

        hit = Physics2D.Raycast(transform.position, new Vector2(0.54f,0.54f), 100f, lmask);       
        Debug.Log(hit.point);
        Debug.Log(hit.collider);

        hit = Physics2D.Raycast(transform.position, new Vector2(-0.71f,0.71f), 100f, lmask);
        Debug.Log(hit.point);
        Debug.Log(hit.collider);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector2.one * 50, Color.red);
        Debug.DrawRay(transform.position, new Vector2(-1, 1) * 50, Color.red);
    }
}

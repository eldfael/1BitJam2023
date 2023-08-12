using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigidbody;
    Vector2 moveDir;
    

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if (moveDir == Vector2.zero)
        {
            if(Input.GetKeyDown("w"))
            {
                moveDir = Vector2.up;

            }else if(Input.GetKeyDown("s"))
            {
                moveDir = Vector2.down;
            }
            else if(Input.GetKeyDown("a"))
            {
                moveDir = Vector2.left;
            }
            else if(Input.GetKeyDown("d"))
            {
                moveDir = Vector2.right;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 v3 = moveDir;
        RaycastHit2D rchit = Physics2D.Raycast(transform.position + v3, moveDir, 0.25f);
        if (rchit.collider == null)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x + moveDir.x, transform.position.y + moveDir.y, transform.position.z)
            , Quaternion.identity);
        }
        else if (rchit.collider.tag == "Pushable")
        {
            Debug.Log("Yep");
            rchit.transform.SetPositionAndRotation(new Vector3(rchit.transform.position.x + moveDir.x, rchit.transform.position.y + moveDir.y, rchit.transform.position.z), Quaternion.identity);
            transform.SetPositionAndRotation(new Vector3(transform.position.x + moveDir.x, transform.position.y + moveDir.y, transform.position.z)
            , Quaternion.identity);
        }
        else if (rchit.collider.tag != "Wall")
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x + moveDir.x, transform.position.y + moveDir.y, transform.position.z)
            , Quaternion.identity);
        }
        
        
        moveDir = Vector2.zero;
    }
}

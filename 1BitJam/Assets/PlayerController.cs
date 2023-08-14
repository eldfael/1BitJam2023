using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigidbody;
    Vector2 moveDir;
    Vector2 target;
    bool moving = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveDir = Vector2.zero;
    }

    private void Update()
    {
        if (moveDir == Vector2.zero)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            if (moveDir.x == 0)
            {
                moveDir.y = Input.GetAxisRaw("Vertical");
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (!moving && moveDir != Vector2.zero)
        {
            target = pos + moveDir;
            moving = true;
            Debug.Log(moveDir);
            Debug.Log(target);
        }
        
        if (moving)
        {
            transform.position = new Vector3 (transform.position.x + moveDir.x/8, transform.position.y + moveDir.y/8, transform.position.z);
            if ((Vector2)transform.position == target)
            {
                Debug.Log("what the hell");
                moving = false;
                moveDir = Vector2.zero;
            }
        }

        //Debug.Log(transform.position);

        /*
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
        
        */
        //moveDir = Vector2.zero;
    }
}

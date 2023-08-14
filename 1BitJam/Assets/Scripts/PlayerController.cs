using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector2 moveDirection;
    Vector2 target;
    Vector2 pos;
    bool moving = false;

    private void Start()
    {
        moveDirection = Vector2.zero;
    }

    private void Update()
    {
        if (moveDirection == Vector2.zero)
        {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            if (moveDirection.x == 0)
            {
                moveDirection.y = Input.GetAxisRaw("Vertical");
            }
        }
    }

    private void FixedUpdate()
    {
        pos = transform.position;
        if (!moving && moveDirection != Vector2.zero)
        {
            target = pos + moveDirection;

            RaycastHit2D raycastHit = Physics2D.BoxCast(target, Vector2.one*0.5f, 0f, Vector2.zero);
            Debug.Log(raycastHit.collider);

            if (raycastHit.collider == null)
            {
                //Empty space ahead - Move as usual
                moving = true;
            }
            else if (raycastHit.collider.tag == "Wall")
            {
                //Wall ahead - don't move
                moveDirection = Vector2.zero;
            }
            else if (raycastHit.collider.tag == "Pushable")
            {
                //Pushable ahead - move and interact with pushable
                if (raycastHit.collider.gameObject.GetComponent<PushableController>().OnPush(moveDirection))
                {
                    //Pushable ahead returned true - Move ahead !
                    moving = true;
                }
                else
                {
                    //Pushable ahead returned false - don't move
                    moveDirection = Vector2.zero;
                }
            }
        }
        
        if (moving)
        {
            transform.position = new Vector3 (transform.position.x + moveDirection.x/8, transform.position.y + moveDirection.y/8, transform.position.z);
            if ((Vector2)transform.position == target)
            {
                moving = false;
                moveDirection = Vector2.zero;
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

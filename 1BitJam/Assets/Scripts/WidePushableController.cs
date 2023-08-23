using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WidePushableController : MonoBehaviour, Pushable
{
    bool moving;
    Vector2 moveDir;
    RaycastHit2D[] raycastHits;
    ContactFilter2D filter;
    Vector2 pos;
    Vector2 target;

    private void Start()
    {
        filter = new ContactFilter2D();
        raycastHits = new RaycastHit2D[2];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(pos + moveDir * 1.5f, Vector3.one * 0.5f);
    }

    public (Vector2, GameObject) OnPush(Vector2 moveDirection)
    {
        Debug.Log(moveDirection);
        pos = transform.position;
        moveDir = moveDirection;
        target = pos + moveDirection;
        moving = true;
        //if moving vertically
        if (moveDirection.y != 0)
        {
            Array.Clear(raycastHits, 0, 2);
            Physics2D.BoxCast(target, new Vector2(1.5f, 0.5f), 0f, Vector2.zero, filter.NoFilter(), raycastHits);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Pushable")
                {
                    raycastHits[i].collider.gameObject.GetComponent<Pushable>().OnPush(moveDirection);
                }
            }

        }
        //if moving horizontally
        else
        {
            Array.Clear(raycastHits, 0, 2);
            Physics2D.BoxCast(pos + moveDirection * 1.5f, Vector2.one * 0.5f, 0f, Vector2.zero, filter.NoFilter(), raycastHits);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Pushable")
                {
                    raycastHits[i].collider.gameObject.GetComponent<Pushable>().OnPush(moveDirection);
                }
            }
        }
        return (pos, gameObject);
    }
    public bool TryPush(Vector2 moveDirection)
    {
        pos = transform.position;
        //if moving vertically
        if (moveDirection.y != 0)
        {
            target = pos + moveDirection;
            Array.Clear(raycastHits, 0, 2);
            Physics2D.BoxCast(target, new Vector2(1.5f, 0.5f), 0f, Vector2.zero, filter.NoFilter(), raycastHits);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Pushable")
                {
                    if (!raycastHits[i].collider.gameObject.GetComponent<Pushable>().TryPush(moveDirection))
                    {
                        return false;
                    }
                }
                else if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Wall")
                {
                    Debug.Log("Wall");
                    return false;
                }
            }
            return true;
        }
        //if moving horizontally
        else
        {
            target = pos + moveDirection * 1.5f;
            Array.Clear(raycastHits, 0, 2);
            Physics2D.BoxCast(target, Vector2.one * 0.5f, 0f, Vector2.zero, filter.NoFilter(), raycastHits);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Pushable")
                {
                    if (!raycastHits[i].collider.gameObject.GetComponent<Pushable>().TryPush(moveDirection))
                    {
                        return false;
                    }
                }
                else if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Wall")
                {
                    return false;
                }
            }
            return true;
        }

    }

    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position = new Vector3(transform.position.x + moveDir.x / 8, transform.position.y + moveDir.y / 8, transform.position.z);
            if ((Vector2)transform.position == target)
            {
                moving = false;
                moveDir = Vector2.zero;
            }
        }
    }

    public void OnUndo()
    {

    }
    
    public bool IsMoving()
    {
        return moving;
    }
}

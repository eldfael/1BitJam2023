using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableController : MonoBehaviour, Pushable
{
    Vector2 pos;
    Vector2 target;
    
    bool moving = false;
    Vector2 moveDirection;
    RaycastHit2D raycastHit;

    public (Vector2,GameObject) OnPush(Vector2 moveDirection)
    {
        Debug.Log("OnPush");
        pos = transform.position;

        this.moveDirection = moveDirection;
        target = pos + moveDirection;
        moving = true;
        raycastHit = Physics2D.BoxCast(target, Vector2.one * 0.5f, 0f, Vector2.zero);
        if (raycastHit.collider != null && raycastHit.collider.tag == "Pushable")
        {
            raycastHit.collider.gameObject.GetComponent<Pushable>().OnPush(moveDirection);
        }
        return (pos, this.gameObject);
    }

    public bool TryPush(Vector2 moveDirection)
    {
        pos = transform.position;
        if (!moving)
        {

            target = pos + moveDirection;
            raycastHit = Physics2D.BoxCast(target, Vector2.one * 0.5f, 0f, Vector2.zero);

            if (raycastHit.collider == null)
            {
                // Nothing ahead - Return true
                return true;

            }
            if (raycastHit.collider.tag == "Wall")
            {
                // Wall ahead - Return false
                return false;
            }
            if (raycastHit.collider.tag == "Pushable")
            {
                // Pushable ahead - Check if Pushable ahead returns true - and if so move ahead and return true
                if (raycastHit.collider.gameObject.GetComponent<Pushable>().TryPush(moveDirection))
                {
                    //Pushable ahead returned true - Return true
                    return true;
                }
                else
                {
                    //Pushable ahead returned false - Return false
                    return false;
                }
            }
        }
        //This should never happen but hey
        return false;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            Debug.Log("Moving");
            transform.position = new Vector3(transform.position.x + moveDirection.x / 8, transform.position.y + moveDirection.y / 8, transform.position.z);
            if ((Vector2)transform.position == target)
            {
                //Debug.Log("Done Moving");
                moving = false;
                moveDirection = Vector2.zero;
            }
        }
    }

    public bool IsMoving()
    {
        return moving;
    }

    public void OnUndo()
    {

    }
}

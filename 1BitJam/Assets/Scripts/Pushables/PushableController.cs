using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableController : MonoBehaviour, Pushable
{
    // GLASS SHOULD BE SET TO TRUE
    public bool breakable = false;

    Vector2 pos;
    Vector2 target;
    
    bool moving = false;
    Vector2 moveDirection;
    RaycastHit2D raycastHit;

    public List<(Vector2, GameObject)> OnPush(Vector2 moveDirection, List<(Vector2, GameObject)> tupleList)
    {
        pos = transform.position;

        if (!tupleList.Contains((pos, gameObject)))
        {
            tupleList.Add((pos, gameObject));
        }

        this.moveDirection = moveDirection;
        target = pos + moveDirection;
        moving = true;
        
        raycastHit = Physics2D.BoxCast(target, Vector2.one * 0.5f, 0f, Vector2.zero);
        if (raycastHit.collider != null && raycastHit.collider.tag == "Pushable")
        {
            tupleList = raycastHit.collider.gameObject.GetComponent<Pushable>().OnPush(moveDirection, tupleList);
        }
        return tupleList;
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
            if (raycastHit.collider.tag == "Wall" || raycastHit.collider.tag == "LightDetector")
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
            transform.position = new Vector3(transform.position.x + moveDirection.x / 8, transform.position.y + moveDirection.y / 8, transform.position.z);
            if ((Vector2)transform.position == target)
            {
                moving = false;
                moveDirection = Vector2.zero;
            }
        }
    }

    public bool IsMoving()
    {
        return moving;
    }

    public bool IsBreakable()
    {
        return breakable;
    }

    public void OnUndo(Vector2 originalPosition)
    {
        gameObject.SetActive(true);
        moving = true;
        target = originalPosition;
        pos = transform.position;
        moveDirection = originalPosition - pos;
    }


}

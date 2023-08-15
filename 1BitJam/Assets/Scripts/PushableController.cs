using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableController : MonoBehaviour
{
    Vector2 pos;
    Vector2 target;
    
    bool moving = false;
    Vector2 moveDirection;

    public bool OnPush(Vector2 moveDirection)
    {
        pos = transform.position;
        if (!moving)
        {
            // if moving horizontally -> and HEIGHT >= 2 then do height number of raycasts
            // if moving vertically -> and WIDTH >= 2 then do width number of raycasts

            target = pos + moveDirection;
            RaycastHit2D raycastHit = Physics2D.BoxCast(target, Vector2.one * 0.5f, 0f, Vector2.zero);

            if (raycastHit.collider == null)
            {
                // Nothing ahead - Move ahead and return true
                this.moveDirection = moveDirection;
                moving = true;
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
                if (raycastHit.collider.gameObject.GetComponent<PushableController>().OnPush(moveDirection))
                {
                    //Pushable ahead returned true - Move ahead and return true
                    this.moveDirection = moveDirection;
                    moving = true;
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
                Debug.Log("Done Moving");
                moving = false;
                moveDirection = Vector2.zero;
            }
        }
    }
}

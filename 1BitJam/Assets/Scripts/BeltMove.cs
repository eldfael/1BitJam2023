using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltMove : MonoBehaviour
{
    public Animator beltAnim;
    public bool beltActive;
    Vector2 moveDirection;
    RaycastHit2D raycastHit;
    void Start()
    {
        float angle = (transform.rotation.z + 90) * Mathf.Deg2Rad;
        moveDirection.x = Mathf.Sin(angle);
        moveDirection.y = Mathf.Cos(angle);
        Debug.Log(moveDirection);
    }

    void FixedUpdate()
    {
        //if (beltActive == true)
        //{
            
        //}
    }

    public void BeltOn()
    {
        beltAnim.SetBool("BeltMoving", true);
        beltActive = true;
        /*raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.05f, 0f, Vector2.zero);
        if (raycastHit.collider != null && raycastHit.collider.tag == "Pushable")
        {   
            Component mv = raycastHit.collider.gameObject.GetComponent<PushableController>();
            if (!mv.moving)
            {
                Debug.Log("something to move");
            }
        }*/
        
    }

    public void BeltOff()
    {
        beltAnim.SetBool("BeltMoving", false);
        beltActive = false;
    }
}

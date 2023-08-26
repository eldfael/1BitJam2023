using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltController : MonoBehaviour
{
    RaycastHit2D raycastHit;
    private void FixedUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.05f, 0f, Vector2.zero);
            if (raycastHit.collider == null || raycastHit.collider.tag == "Wall")
            {
                transform.GetChild(i).gameObject.GetComponent<BeltMove>().BeltOff();
            }
            else
            {
                transform.GetChild(i).gameObject.GetComponent<BeltMove>().BeltOn();
            }
        }

        /*raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.05f, 0f, Vector2.zero);
        if (raycastHit.collider != null)
        {   
            for (int i = 0; i < transform.childCount; i++)
            {
                //transform.GetChild(i).gameObject.SetActive(false);
            }
        
        }*/

    }
}

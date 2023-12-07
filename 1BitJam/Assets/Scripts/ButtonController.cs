using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    RaycastHit2D raycastHit;
    private void FixedUpdate()
    {
        raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0f, Vector2.zero);
        if (raycastHit.collider != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                raycastHit = Physics2D.BoxCast(transform.GetChild(i).transform.position, Vector2.one * 0.8f, 0f, Vector2.zero);
                if (raycastHit.collider == null || raycastHit.collider.tag == "Wall")
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

    }
}

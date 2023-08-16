using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    bool buttonState = false;
    private void FixedUpdate()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.05f, 0f, Vector2.zero);
        if (raycastHit.collider != null)
        {
            // if there is object on top of button - open doors
            if (!buttonState)
            {
                buttonState = true;
                for (int i = 0; i< transform.childCount; i++)
                {
                    //Temporary open and close for doors - can be changed later
                    transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
                    transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            
        }
        else
        {
            // no object on top of button - close doors if open
            if (buttonState)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    //Temporary open and close for doors - can be changed later
                    transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                    transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                }
                buttonState = false;
            }
        }
    }
}

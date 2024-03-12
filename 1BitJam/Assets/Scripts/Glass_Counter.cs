using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Counter : MonoBehaviour
{
    int glass_left = 0;
    public int UnlockNumber = 0;
    RaycastHit2D raycastHit;
    /*void Start()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Pushable").Length);
    }*/

    private void FixedUpdate()
    {
        var pushables = GameObject.FindGameObjectsWithTag("Pushable");
        glass_left = 0;
        for (int i = 0; i < pushables.Length; i++)
        {
            if (pushables[i].GetComponent<BoxCollider2D>().enabled && pushables[i].GetComponent<Pushable>().IsBreakable())
            {
                glass_left++;
            }
        }
        //Debug.Log(glass_left);
        if (glass_left <= UnlockNumber)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                //transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i).GetChild(0).gameObject.GetComponent<DoorUnlock>().doorState(true); // note that TRUE means the door is UNLOCKED (ie something on the button)
                transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                raycastHit = Physics2D.BoxCast(transform.GetChild(i).transform.position, Vector2.one * 0.8f, 0f, Vector2.zero);
                if (raycastHit.collider == null || raycastHit.collider.tag == "Wall")
                {
                    //transform.GetChild(i).gameObject.SetActive(true);
                    transform.GetChild(i).GetChild(0).gameObject.GetComponent<DoorUnlock>().doorState(false); // false means door is LOCKED
                    transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                {
                    //transform.GetChild(i).gameObject.SetActive(false);
                    transform.GetChild(i).GetChild(0).gameObject.GetComponent<DoorUnlock>().doorState(true);
                    transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }

    }
}

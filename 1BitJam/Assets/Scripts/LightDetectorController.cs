using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetectorController : MonoBehaviour
{
    bool dontClose;
    bool buttonState = false;
    bool closed;

    bool open;
    bool hit;
    LightCast[] casters;
    RaycastHit2D raycastHit;

    private void Start()
    {
        casters = FindObjectsOfType<LightCast>();
        open = false;
        hit = false;
    }
    private void FixedUpdate()
    {
        for (int i = 0; i< casters.Length; i++) 
        {
            if (casters[i].CheckLight(this.gameObject))
            {
                open = true;
                hit = true;
            }
        }
        if (!hit)
        {
            open = false;
        }
        hit = false;

        for (int i = 0; i< transform.childCount; i++)
        {
            if (!open)
            {
                raycastHit = Physics2D.BoxCast(transform.GetChild(i).transform.position, Vector2.one * 0.05f, 0f, Vector2.zero);
                if (raycastHit.collider == null || raycastHit.collider.tag == "Wall")
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }



    public void SetState(bool state)
    {
        buttonState = state;
    }

}

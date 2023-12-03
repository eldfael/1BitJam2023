using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetectorController : MonoBehaviour
{
    bool dontClose;
    bool buttonState = false;
    bool closed;

    bool open;
    LightCast[] casters;
    RaycastHit2D raycastHit;

    private void Start()
    {
        casters = FindObjectsOfType<LightCast>();
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < casters.Length; j++)
            { 
                if (casters[j].CheckLight(this.gameObject))
                {

                    open = true;
                    // Open if light is hitting
                }
                else
                {
                    raycastHit = Physics2D.BoxCast(transform.GetChild(i).transform.position, Vector2.one * 0.05f, 0f, Vector2.zero);
                    if (raycastHit.collider == null || raycastHit.collider.tag == "Wall")
                    {
                        open = false;
                    }
                    else
                    {
                        open = true;
                    }
                }
            }
            if (open)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            else 
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void SetState(bool state)
    {
        buttonState = state;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetectorController : MonoBehaviour
{
    bool dontClose;
    bool buttonState = false;
    bool closed;
    LightCast[] casters;
    private void FixedUpdate()
    {

        if (buttonState)        
        {                  
            for (int i = 0; i < transform.childCount; i++)            
            {
                //Temporary open and close for doors - can be changed later                
                transform.GetChild(i).gameObject.SetActive(false);            
            }
            closed = false;
            buttonState = false;
           
        }
            // no object on top of button - close doors if open
            
        else if (!buttonState)        
        {
            if(!closed)
            {
                dontClose = false;
                //StartCoroutine(WaitToClose());
                casters = FindObjectsOfType<LightCast>();
                for (int i = 0; i < casters.Length; i++)
                {
                    if (casters[i].CheckLight(this.gameObject))
                    {
                        dontClose = true;
                        Debug.Log("Flicker protection working lol");
                    }
                }

                if (!dontClose)
                {
                    closed = true;
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        //Temporary open and close for doors - can be changed later                
                        transform.GetChild(i).gameObject.SetActive(true);
                        Debug.Log("Closed");
                    }

                }
            }
             
        }

    }


    public void SetState(bool state)
    {
        buttonState = state;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetectorController : MonoBehaviour
{
    bool buttonState = false;
    private void FixedUpdate()
    {
        
        if (buttonState)        
        {                  
            for (int i = 0; i < transform.childCount; i++)            
            {           
                //Temporary open and close for doors - can be changed later                
                transform.GetChild(i).GetComponent<Collider2D>().enabled = false;               
                transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;               
            }
            buttonState = false;
           
        }
            // no object on top of button - close doors if open
            
        else if (!buttonState)        
        {        
            for (int i = 0; i < transform.childCount; i++)            
            {            
                //Temporary open and close for doors - can be changed later                
                transform.GetChild(i).GetComponent<Collider2D>().enabled = true;                
                transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;             
            }    
        }
    }

    public void SetState(bool state)
    {
        buttonState = state;
    }

}

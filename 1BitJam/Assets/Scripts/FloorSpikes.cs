using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpikes : MonoBehaviour
{
    RaycastHit2D raycastHit;
    PlayerController playercon;
    Pushable pushablecon;
    LayerMask lmask;
    public Animator WarningSign;

    private void Start()
    {
        lmask = LayerMask.GetMask("Default") + LayerMask.GetMask("TransparentFX") + LayerMask.GetMask("AxeBlock") + LayerMask.GetMask("AxeBox");    
    }
    private void FixedUpdate()
    {
        raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0f, Vector2.zero, Mathf.Infinity, lmask);
        if (raycastHit.collider != null)
        {
            //Debug.Log(raycastHit.collider);
            if (raycastHit.collider.tag == "Player")
            {
                WarningSign.SetBool("warning",false);
                playercon = raycastHit.collider.gameObject.GetComponent<PlayerController>();
                if (!playercon.IsMoving() && playercon.control)
                {
                    playercon.PlayerDeath();
                }
            }
            else if (raycastHit.collider.tag == "Pushable")
            {
                pushablecon = raycastHit.collider.gameObject.GetComponent<Pushable>();
                if (!pushablecon.IsMoving() && pushablecon.IsBreakable())
                {
                    Debug.Log(2);
                    WarningSign.SetBool("warning",false);
                    // BREAK ANIMATION
                    raycastHit.collider.GetComponent<Pushable>().OnBreak();

                    //raycastHit.collider.gameObject.GetComponent<Animator>().SetBool("smash",true);
                    //raycastHit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    //StartCoroutine(WaitToBreak(raycastHit.collider.gameObject));
                    //raycastHit.collider.gameObject.SetActive(false);
                }
                else if(pushablecon.IsBreakable())
                {
                    WarningSign.SetBool("warning", false);
                }
                else 
                {
                    WarningSign.SetBool("warning",true);
                }
            }
            else if (raycastHit.collider.tag == "AxeBox")
            {
                WarningSign.SetBool("warning", true);
            }
        } 
        else 
        {
            WarningSign.SetBool("warning",false);
        }
        //Debug.Log(raycastHit.collider);
    }

    /*IEnumerator WaitToBreak(GameObject glass)
    {
        yield return new WaitForSeconds(0.4f);
        if (glass.GetComponent<Animator>().GetBool("smash") == false) {glass.SetActive(false);}
        
    }*/

}

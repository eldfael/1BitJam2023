using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSpikes : MonoBehaviour
{
    RaycastHit2D raycastHit;
    PlayerController playercon;
    Pushable pushablecon;
    LayerMask lmask;

    private void Start()
    {
        lmask = LayerMask.GetMask("Default") + LayerMask.GetMask("TransparentFX");
    }
    private void FixedUpdate()
    {
        raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0f, Vector2.zero, Mathf.Infinity, lmask);
        if (raycastHit.collider != null)
        {
            //Debug.Log(raycastHit.collider);
            if (raycastHit.collider.tag == "Player")
            {
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
                    // BREAK ANIMATION
                    raycastHit.collider.GetComponent<Pushable>().OnBreak();

                    //raycastHit.collider.gameObject.GetComponent<Animator>().SetBool("smash",true);
                    //raycastHit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    //StartCoroutine(WaitToBreak(raycastHit.collider.gameObject));
                    //raycastHit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
}



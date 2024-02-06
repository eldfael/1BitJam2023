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
        lmask = LayerMask.GetMask("Default") + LayerMask.GetMask("TransparentFX");
        
    }
    private void FixedUpdate()
    {
        raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0f, Vector2.zero, Mathf.Infinity, lmask);
        if (raycastHit.collider != null)
        {
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
                    WarningSign.SetBool("warning",false);
                    // BREAK ANIMATION
                    raycastHit.collider.gameObject.SetActive(false);
                } else {
                    WarningSign.SetBool("warning",true);
                }
            }
        } else {
            WarningSign.SetBool("warning",false);
        }
        Debug.Log(raycastHit.collider);
    }

}

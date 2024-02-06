using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpikes : MonoBehaviour
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
                    // REPLACE THIS WITH A DELAY + ANIMATION ON THE GLASS BLOCKS LATER
                    raycastHit.collider.gameObject.SetActive(false);
                }
            }
        }
        Debug.Log(raycastHit.collider);
    }

}

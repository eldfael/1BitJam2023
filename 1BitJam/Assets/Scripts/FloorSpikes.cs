using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpikes : MonoBehaviour
{
    RaycastHit2D raycastHit;
    PlayerController pcon;

    private void FixedUpdate()
    {
        raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.2f, 0f, Vector2.zero);
        if (raycastHit.collider != null && raycastHit.collider.tag == "Player")
        {
            pcon = raycastHit.collider.gameObject.GetComponent<PlayerController>();
            if (!pcon.IsMoving() && pcon.control)
            {
                pcon.PlayerDeath();
            }
        }
    }

}

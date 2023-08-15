using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    private void FixedUpdate()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, Vector2.zero);
        if (raycastHit.collider != null && raycastHit.collider.tag == "Player")
        {
            raycastHit.collider.gameObject.GetComponent<PlayerController>().PlayerWin();
        }
    }
}

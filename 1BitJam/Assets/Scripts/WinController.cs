using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    public string GoToLevel = "";
    private void FixedUpdate()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, Vector2.zero);
        if (raycastHit.collider != null && raycastHit.collider.tag == "Player")
        {
            /*int WinLevel = (GoToLevel > 0) ? (GoToLevel % 100) + 2 : 0;
            if (GoToLevel > 100) {
                WinLevel += 6 + ((GoToLevel-GoToLevel%100)/100-1)*30;
            }*/
            raycastHit.collider.gameObject.GetComponent<PlayerController>().PlayerWin(GoToLevel);
        }
    }
}

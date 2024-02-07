using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNavigator : MonoBehaviour
{

    Vector2 dir;

    Vector2 target;
    Vector2 pos;
    Vector2 movedir;
    bool moving;
    bool readyToMove;
    RaycastHit2D hit;
    int counter;

    private void Start()
    {
        moving = false;
        readyToMove = true;
        dir = Vector2.zero;
        counter = 0;
    }
    private void Update()
    {
        if (!moving && readyToMove)
        {
            if (Input.GetKey("space") || Input.GetKey("z"))
            {
                hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0f, Vector2.zero);
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<LevelSelect>().GoToLevel();
                }
            }

            dir.x = Input.GetAxisRaw("Horizontal");
            if (dir.x != 0)
            {
                hit = Physics2D.Raycast((Vector2)transform.position + dir, dir, 10f);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider);
                    target = (Vector2)hit.collider.transform.position;
                    pos = (Vector2)transform.position;
                    movedir = target - pos;
                    moving = true;
                    readyToMove = false;
                    Debug.Log(target);
                    Debug.Log(pos);
                    Debug.Log(movedir);

                }
            }
            else
            {
                dir.y = Input.GetAxisRaw("Vertical");
                if (dir.y != 0)
                {
                    hit = Physics2D.Raycast((Vector2)transform.position + dir, dir, 10f);
                    if (hit.collider != null)
                    {
                        Debug.Log(hit.collider);
                        target = (Vector2)hit.collider.transform.position;
                        pos = (Vector2)transform.position;
                        movedir = target - pos;
                        moving = true;
                        readyToMove = false;

                    }
                }
            }

            dir = Vector2.zero;
        }
        if (moving)
        {
            transform.position = new Vector3(transform.position.x + movedir.x / 60, transform.position.y + movedir.y / 60, -1);
            counter++;
            if ((Vector2)transform.position == target || counter == 60)
            {
                transform.position = new Vector3(target.x,target.y,-1);
                counter = 0;
                Debug.Log("Done Moving");
                moving = false;
                StartCoroutine(WaitToMove());
            }
        }
    }

    private void FixedUpdate()
    {

    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(0.08f);
        readyToMove = true;
    }
}

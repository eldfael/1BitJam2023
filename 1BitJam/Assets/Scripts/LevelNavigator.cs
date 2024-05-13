using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelNavigator : MonoBehaviour
{


    Vector2 dir;

    Vector2 target;
    Vector2 pos;
    Vector2 movedir;
    bool moving;
    bool readyToMove;
    RaycastHit2D hit;
    float counter;
    float speed;

    private void Start()
    {
        speed = 0.1f; // Lower speed = Faster movement - yeah deal with it
        moving = false;
        readyToMove = false;
        StartCoroutine(WaitToMove());
        dir = Vector2.zero;
        counter = 0;
    }
    private void Update()
    {

        if (!moving && readyToMove)
        {
            if (Input.GetKeyDown("escape"))
            {
                SceneManager.LoadScene("Top Menu");
            }

            if (Input.GetKeyDown("space") || Input.GetKeyDown("z"))
            {
                Debug.Log("KeyDown");
                hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0f, Vector2.zero);
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<LevelSelect>().GoToLevel();
                }
            }

            dir.x = Input.GetAxisRaw("Horizontal");
            if (dir.x != 0)
            {
                hit = Physics2D.Raycast((Vector2)transform.position + dir, dir, 1f);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Warp")
                    {
                        hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0f, Vector2.zero);
                        if (hit.collider != null)
                        {
                            hit.collider.gameObject.GetComponent<LevelSelect>().GoToLevel();
                        }
                    }
                    else
                    {
                        target = (Vector2)hit.collider.transform.position;
                        pos = (Vector2)transform.position;
                        movedir = target - pos;
                        moving = true;
                        readyToMove = false;
                    }
                }
            }
            else
            {
                dir.y = Input.GetAxisRaw("Vertical");
                if (dir.y != 0)
                {
                    hit = Physics2D.Raycast((Vector2)transform.position + dir, dir, 1f);
                    if (hit.collider != null)
                    {

                        if (hit.collider.tag == "Warp")
                        {
                            hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0f, Vector2.zero);
                            if (hit.collider != null)
                            {
                                hit.collider.gameObject.GetComponent<LevelSelect>().GoToLevel();
                            }
                        }
                        else
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
            }
            

            dir = Vector2.zero;
        }
        if (moving)
        {
            transform.position = new Vector3(transform.position.x + (movedir.x * Time.deltaTime) / speed, transform.position.y + (movedir.y * Time.deltaTime) / speed, -1);
            counter += Time.deltaTime;
            if ((Vector2)transform.position == target || counter >= speed)
            {
                transform.position = new Vector3(target.x, target.y, -1);
                counter = 0;
                moving = false;
                StartCoroutine(WaitToMove());
            }
        }
    }

    private void FixedUpdate()
    {
        // Move this to Update with Time.Deltatime added at a later date to smooth movement out
        // Legacy code - this has been moved to Update
        /*
        if (moving)
        {
            transform.position = new Vector3(transform.position.x + movedir.x / speed, transform.position.y + movedir.y / speed, -1);
            counter++;
            if ((Vector2)transform.position == target || counter == speed)
            {
                transform.position = new Vector3(target.x, target.y, -1);
                counter = 0;
                moving = false;
                StartCoroutine(WaitToMove());
            }
        }
        */
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(0.08f);
        readyToMove = true;
    }

}

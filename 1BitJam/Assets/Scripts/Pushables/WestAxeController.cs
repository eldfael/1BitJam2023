using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WestAxeController : MonoBehaviour, Pushable
{
    // GLASS SHOULD BE SET TO TRUE
    public bool breakable = false;

    bool moving;
    Vector2 moveDir;
    RaycastHit2D[] raycastHits;
    ContactFilter2D filter;
    Vector2 pos;
    Vector2 target;

    PlayerController pc;
    FloorSpikes spikes;

    LayerMask lmask;

    private void Start()
    {
        filter = new ContactFilter2D();
        raycastHits = new RaycastHit2D[2];
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        spikes = GetComponent<FloorSpikes>();

    }
    public List<(Vector2, GameObject)> OnPush(Vector2 moveDirection, List<(Vector2, GameObject)> tupleList)
    {
        pos = transform.position;
        if (!tupleList.Contains((pos, gameObject)))
        {
            tupleList.Add((pos, gameObject));
        }

        moveDir = moveDirection;
        target = pos + moveDirection;
        moving = true;
        //if moving vertically
        if (moveDirection.y != 0)
        {
            Array.Clear(raycastHits, 0, 2);
            Physics2D.BoxCast(target, new Vector2(1.5f, 0.5f), 0f, Vector2.zero, filter.NoFilter(), raycastHits);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Pushable")
                {
                    if (raycastHits[i].collider.transform.position.x <= transform.position.x - 0.5 && raycastHits[i].collider.GetComponent<Pushable>().IsBreakable())
                    {
                        raycastHits[i].collider.GetComponent<Pushable>().OnBreak();
                        tupleList.Add(((Vector2)raycastHits[i].collider.transform.position, raycastHits[i].collider.gameObject));
                    }
                    else
                    {
                        tupleList = raycastHits[i].collider.gameObject.GetComponent<Pushable>().OnPush(moveDirection, tupleList);
                    }
                }
            }

        }
        //if moving horizontally
        else
        {
            Array.Clear(raycastHits, 0, 2);
            Physics2D.BoxCast(pos + moveDirection * 1.5f, Vector2.one * 0.5f, 0f, Vector2.zero, filter.NoFilter(), raycastHits);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Pushable")
                {
                    if (moveDirection.x <= -1 && raycastHits[i].collider.GetComponent<Pushable>().IsBreakable())
                    {
                        raycastHits[i].collider.GetComponent<Pushable>().OnBreak();
                        tupleList.Add(((Vector2)raycastHits[i].collider.transform.position, raycastHits[i].collider.gameObject));
                    }
                    else
                    {
                        tupleList = raycastHits[i].collider.gameObject.GetComponent<Pushable>().OnPush(moveDirection, tupleList);
                    }

                }
            }
        }
        return tupleList;
    }
    public bool TryPush(Vector2 moveDirection)
    {
        pos = transform.position;
        //Check to see if player is trying to push Axe
        if ((pc.transform.position.x <= pos.x - 0.5 && pc.transform.position.x >= pos.x - 1.5) && (pc.transform.position.y <= pos.y + 1 && pc.transform.position.y >= pos.y - 1))
        {
            //Player is trying to push Axe - Dont push and move player onto axe
            pc.MoveOntoAxe();
            return false;
        }

        //if moving vertically
        if (moveDirection.y != 0)
        {
            target = pos + moveDirection;
            Array.Clear(raycastHits, 0, 2);
            Physics2D.BoxCast(target, new Vector2(1.5f, 0.5f), 0f, Vector2.zero, filter.NoFilter(), raycastHits);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Pushable")
                {
                    if (raycastHits[i].collider.transform.position.x <= transform.position.x - 0.5 && raycastHits[i].collider.GetComponent<Pushable>().IsBreakable())
                    {
                        // Glass ahead on Axe side - Break on push
                        // Continue checks
                    }
                    else if (!raycastHits[i].collider.gameObject.GetComponent<Pushable>().TryPush(moveDirection))
                    {
                        return false;
                    }
                }
                else if (raycastHits[i].collider != null && (raycastHits[i].collider.tag == "Wall" || raycastHits[i].collider.tag == "LightDetector"))
                {
                    return false;
                }
            }
            return true;
        }
        //if moving horizontally
        else
        {
            target = pos + moveDirection * 1.5f;
            Array.Clear(raycastHits, 0, 2);
            Physics2D.BoxCast(target, Vector2.one * 0.5f, 0f, Vector2.zero, filter.NoFilter(), raycastHits);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider != null && raycastHits[i].collider.tag == "Pushable")
                {
                    if (moveDirection.x <= -1 && raycastHits[i].collider.GetComponent<Pushable>().IsBreakable())
                    {
                        // Glass ahead - Break on push
                        // Will return True
                    }
                    else if (!raycastHits[i].collider.gameObject.GetComponent<Pushable>().TryPush(moveDirection))
                    {
                        return false;
                    }
                }
                else if (raycastHits[i].collider != null && (raycastHits[i].collider.tag == "Wall" || raycastHits[i].collider.tag == "LightDetector"))
                {
                    return false;
                }
            }
            return true;
        }

    }

    private void FixedUpdate()
    {
        if (moving)
        {
            spikes.enabled = false;
            transform.position = new Vector3(transform.position.x + moveDir.x / 8, transform.position.y + moveDir.y / 8, transform.position.z);
            if ((Vector2)transform.position == target)
            {
                moving = false;
                moveDir = Vector2.zero;
            }
        }
        else
        {
            spikes.enabled = true;
        }

    }

    public void OnUndo(Vector2 originalPosition)
    {
        gameObject.SetActive(true);
        if (breakable)
        {
            this.gameObject.GetComponent<Animator>().SetBool("smash", false);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        moving = true;
        target = originalPosition;
        pos = transform.position;
        moveDir = originalPosition - pos;
    }

    public bool IsMoving()
    {
        return moving;
    }

    public bool IsBreakable()
    {
        return breakable;
    }

    public void OnBreak()
    {

    }

    public Vector2 GetAxe()
    {
        return new Vector2(transform.position.x - 0.5f, transform.position.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    public GameController gc;
    
    // Select mode
    // 1: Keyboard
    // 2: Mouse Hovering
    // Mouse by default
    public int selectMode = 2;
    
    MenuObject[] menuObjects;
    public int current;
    bool readyToMove;


    private void Start()
    {
        menuObjects = new MenuObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            menuObjects[i] = transform.GetChild(i).GetComponent<MenuObject>();
        }
        current = -1;
        readyToMove = true;
    }
    private void Update()
    {
        if (readyToMove)
        {
            if (Input.GetKey("space") || Input.GetKey("z"))
            {
                if (current != -1)
                {
                    menuObjects[current].OnSelect();
                }
            }
            else if (Input.GetAxisRaw("Vertical") == 1)
            {
                if (current == -1)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        menuObjects[i].Highlight(false);
                    }
                    // Start at top of Menu Objects
                    current = 0;
                    menuObjects[current].Highlight(true);
                }
                else if (current == 0)
                {
                    menuObjects[current].Highlight(false);
                    current = transform.childCount - 1;
                    menuObjects[current].Highlight(true);
                }
                else
                {
                    menuObjects[current].Highlight(false);
                    current--;
                    menuObjects[current].Highlight(true);
                }
                readyToMove = false;
                StartCoroutine(WaitToSelect());
            }
            else if (Input.GetAxisRaw("Vertical") == -1)
            {
                if (current == -1)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        menuObjects[i].Highlight(false);
                    }
                    // Start at top of Menu Objects
                    current = 0;
                    menuObjects[current].Highlight(true);
                }
                else if (current == transform.childCount - 1)
                {
                    menuObjects[current].Highlight(false);
                    current = 0;
                    menuObjects[current].Highlight(true);
                }
                else
                {
                    menuObjects[current].Highlight(false);
                    current++;
                    menuObjects[current].Highlight(true);
                }
                readyToMove = false;
                StartCoroutine(WaitToSelect());
            }           
        }       
    }

    public void SwapToMouse()
    {
        if (current != -1)
        {
            menuObjects[current].Highlight(false);
            current = -1;
        }             
    }
       

    IEnumerator WaitToSelect()
    {
        yield return new WaitForSeconds(0.1f);
        readyToMove = true;
    }

    public void Reset()
    {
        current = -1;
        readyToMove = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            menuObjects[i].Highlight(false);
        }
    }
}

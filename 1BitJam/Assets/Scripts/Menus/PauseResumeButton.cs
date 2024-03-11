using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseResumeButton : MonoBehaviour, MenuObject, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public GameController gc;
    public bool highlighted;
    public int i;
    MenuSelector ms;

    private void Start()
    {
        ms = transform.parent.GetComponent<MenuSelector>();
        //gc = ms.gc;
        highlighted = false;
    }

    private void Update()
    {
        if(highlighted)
        {
            // Stuff to do here later!!
            //Debug.Log("Resume button is highlighted");
            Debug.Log(i);
            
        }
    }

    public void OnSelect()
    {
        gc.OnGameUnpause();
    }

    public void Highlight(bool b)
    {
        highlighted = b;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlighted = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlighted = true;
        ms.SwapToMouse();
    }
}

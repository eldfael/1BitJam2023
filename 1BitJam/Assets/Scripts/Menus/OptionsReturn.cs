using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsReturn : MonoBehaviour, MenuObject, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler, IPointerMoveHandler
{
    //public GameController gc;
    public bool highlighted;
    MenuSelector ms;
    public Menu m;

    public Sprite normalSprite;
    public Sprite highlightedSprite;

    Image im;

    private void Start()
    {
        im = GetComponent<Image>();
        ms = transform.parent.GetComponent<MenuSelector>();
        //gc = ms.gc;
        highlighted = false;
    }

    private void Update()
    {
        if (highlighted)
        {
            // Stuff to do here later!!
            //Debug.Log("Resume button is highlighted");
            im.sprite = highlightedSprite;

        }
        else
        {
            im.sprite = normalSprite;
        }
    }

    public void OnSelect()
    {
        m.CloseOptionsMenu();
        ms.Reset();
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

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!highlighted)
        {
            highlighted = true;
            ms.SwapToMouse();
        }
    }

    public void OnSlide(int i)
    {

    }
}


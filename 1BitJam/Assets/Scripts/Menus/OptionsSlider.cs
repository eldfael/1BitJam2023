using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsSlider : MonoBehaviour, MenuObject, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler, IPointerMoveHandler
{
    //public GameController gc;
    public Menu m;
    public bool highlighted;
    MenuSelector ms;

    public Sprite normalSprite;
    public Sprite highlightedSprite;

    Image im;
    Slider slide;

    private void Start()
    {
        im = GetComponent<Image>();
        slide = GetComponentInChildren<Slider>();
        ms = transform.parent.GetComponent<MenuSelector>();
        //gc = ms.gc;
        highlighted = false;

    }

    private void Update()
    {
        // Need sprites for this / rewrite code specifically for this part
        /*
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
        */
    }

    public void OnSelect()
    {
        // maybe do muting here ?
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
        ms.lastobject = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlighted = true;
        ms.SwapToMouse();
        ms.lastobject = this.gameObject;
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
        if (i == 1)
        {
            if (slide.value != slide.maxValue)
            {
                slide.value += 1;
            }
        }
        else
        {
            if (slide.value != slide.minValue)
            {
                slide.value -= 1;
            }
        }
    }
}

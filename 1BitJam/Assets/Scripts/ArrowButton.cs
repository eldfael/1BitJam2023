using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    public bool buttonPressed;
    public int xdir = 0;
    public int ydir = 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public int GetX()
    {
        return xdir;
    }

    public int GetY()
    {
        return ydir;
    }


}


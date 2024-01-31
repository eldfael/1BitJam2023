using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UndoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    public bool buttonPressed;

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


}


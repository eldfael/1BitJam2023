using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UndoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    public bool buttonPressed;
    PlayerController pcon;

    private void Start()
    {
        pcon = GameObject.FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (buttonPressed && pcon.control && pcon.readyToMove)
        {
            pcon.OnUndo();
        }
    }

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


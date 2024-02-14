using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DeadUndotButton : MonoBehaviour, IPointerDownHandler
{

    public bool buttonPressed;
    bool clickable;
    PlayerController pcon;
    private void Start()
    {
        pcon = GameObject.FindFirstObjectByType<PlayerController>();
        clickable = false;
    }

    private void Update()
    {
        if (!pcon.control)
        {
            clickable = true;
        }
        else
        {
            clickable = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickable && pcon.readyToMove)
        {
            pcon.OnUndo();
        }
    }



}


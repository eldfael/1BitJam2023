using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LeftArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{

    public bool buttonPressed;
    PlayerController pcon;

    private void Start()
    {
        pcon = GameObject.FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (buttonPressed)
        {
            if (pcon.readyToMove && pcon.control && pcon.moveDirection == Vector2.zero)
            {
                pcon.MoveLeft();
            }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.touchCount >= 1)
        {
            buttonPressed = true;
            Debug.Log("Left Pressed");
        }
    }

}


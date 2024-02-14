using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuButton : MonoBehaviour, IPointerDownHandler
{

    public bool buttonPressed;
    GameController gc;

    public void Start()
    {
        gc = GameObject.FindFirstObjectByType<GameController>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        gc.menuButton = true;
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Unlock : MonoBehaviour, IPointerDownHandler
{
    public GameController gc;
    public void OnPointerDown(PointerEventData eventData)
    {
        gc.cheat = true;
        this.gameObject.SetActive(false);
    }
}

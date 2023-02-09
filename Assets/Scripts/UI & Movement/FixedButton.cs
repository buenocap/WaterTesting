using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public bool Pressed;
    public bool Clicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        Clicked = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked = true;
    }


}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEventsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action onDown;
    public Action onUp;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        onDown?.Invoke();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        onUp?.Invoke();
    }
}

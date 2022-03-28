using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputCatcher : MonoBehaviour, IPointerDownHandler
{
    public static UnityEvent TapEvent = new UnityEvent();
    
    public void OnPointerDown(PointerEventData eventData)
    {
        TapEvent.Invoke();
    }
}

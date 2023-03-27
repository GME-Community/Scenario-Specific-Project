using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickImageEventListener : UnityEngine.EventSystems.EventTrigger
{
    public UnityAction<GameObject> onClick;

    public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }

    static public ClickImageEventListener Get(GameObject go)
    {
        ClickImageEventListener listener = go.GetComponent<ClickImageEventListener>();
        if (listener == null)
            listener = go.AddComponent<ClickImageEventListener>();

        return listener;
    }
}



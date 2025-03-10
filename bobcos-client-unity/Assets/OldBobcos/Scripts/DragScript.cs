using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragScript : MonoBehaviour,IDragHandler
{
    public RectTransform rect;
    public float xpos;

    public void Start()
    {
        xpos = rect.anchoredPosition.x;
    }
    public void OnDrag(PointerEventData data)
    {

        rect.anchoredPosition += data.delta;

        rect.anchoredPosition = new Vector2(xpos, rect.anchoredPosition.y);
    }
  
}

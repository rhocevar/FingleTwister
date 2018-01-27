using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
		if (eventData.dragging)
		{
			Debug.Log (eventData.pointerCurrentRaycast.worldPosition);
			transform.position = eventData.pointerCurrentRaycast.worldPosition;
		}
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}

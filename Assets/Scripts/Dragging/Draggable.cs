using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Dragging
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[Inject]

		private new Rigidbody2D rigidbody;

		private void Awake ()
		{
			rigidbody = GetComponent<Rigidbody2D> ();
		}

		public void OnBeginDrag (PointerEventData eventData)
		{

		}

		public void OnDrag (PointerEventData eventData)
		{
			if (eventData.pointerCurrentRaycast.gameObject == gameObject)
			{
				//Debug.Log (eventData.pointerCurrentRaycast.worldPosition);
				rigidbody.position = eventData.pointerCurrentRaycast.worldPosition;
			}
		}

		public void OnEndDrag (PointerEventData eventData)
		{

		}
	}
}
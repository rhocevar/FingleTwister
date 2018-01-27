using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Zenject;

namespace Dragging
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[Inject]
		private FingerTracker fingers;

		private new Rigidbody2D rigidbody;
		private int? fingerId;
		private Vector3? fingerPosition;

		private void Awake ()
		{
			rigidbody = GetComponent<Rigidbody2D> ();
		}

		public void OnBeginDrag (PointerEventData eventData)
		{
			fingerId = eventData.pointerId;
			Debug.Log (string.Format ("Start Drag: {0}", fingerId.Value));
		}

		public void OnDrag (PointerEventData eventData)
		{
			if (eventData.pointerCurrentRaycast.gameObject == gameObject)
				rigidbody.position = eventData.pointerCurrentRaycast.worldPosition;
			else if (fingerId != null)
				TryMatchFinger ();
		}

        private void TryMatchFinger()
        {
			if (fingers.TryGetWorldPosition (fingerId.Value, out fingerPosition))
				rigidbody.position = fingerPosition.Value;
			else
				fingerId = null;
        }

        public void OnEndDrag (PointerEventData eventData)
		{
			Debug.Log (string.Format ("End Drag: {0}", fingerId.Value));
			fingerId = null;
		}
	}
}
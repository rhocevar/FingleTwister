using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Zenject;
using Power;

namespace Dragging
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
        [SerializeField] private float pullBackSpeed = 1.0f;

		[Inject]
		private FingerTracker fingers;
        [Inject] PowerController powerController;
		private new Rigidbody2D rigidbody;
		private int? fingerId;
		private Vector3? fingerPosition;
        private Vector3? startPosition = null; //This object's initial position
        private bool isDragging = false;
        private float animTime = 0.0f;

		private void Awake ()
		{
			rigidbody = GetComponent<Rigidbody2D> ();
		}

		public void OnBeginDrag (PointerEventData eventData)
		{
            //Store object's initial position
            if (startPosition == null && eventData.pointerCurrentRaycast.gameObject == gameObject)
                startPosition = gameObject.transform.position;
            
			fingerId = eventData.pointerId;
            isDragging = true;
			//Debug.Log (string.Format ("Start Drag: {0}", fingerId.Value));
		}

		public void OnDrag (PointerEventData eventData)
		{
            if (eventData.pointerCurrentRaycast.gameObject == gameObject && powerController.IsOn)
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
            isDragging = false;
            DragBack();
			//Debug.Log (string.Format ("End Drag: {0}", fingerId.Value));
			fingerId = null;
		}

        //Pull the object back to initial position when player stop dragging
        private void DragBack()
        {
            StartCoroutine(AnimDragBack());
        }

        private IEnumerator AnimDragBack()
        {
            Vector3 currentPosition = transform.position;
            Vector3 interpolatedPosition = Vector3.zero;
            animTime = 0.0f;

            while(!isDragging && animTime < 1.0f)
            {
                yield return new WaitForEndOfFrame();

                interpolatedPosition.x = EasingFunctions.EaseOutExpo(currentPosition.x, startPosition.Value.x, animTime);
                interpolatedPosition.y = EasingFunctions.EaseOutExpo(currentPosition.y, startPosition.Value.y, animTime);
                //interpolatedPosition.z = EasingFunctions.EaseOutExpo(currentPosition.z, startPosition.Value.z, animTime); No need because it's 2D

                transform.position = interpolatedPosition;

                //Debug.Log($"Object pos = ({interpolatedPosition.x}, {interpolatedPosition.y} animTime = {animTime}");

                animTime += Time.deltaTime * pullBackSpeed;
            }
        }
	}
}
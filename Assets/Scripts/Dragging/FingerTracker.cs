using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Dragging
{
	public class FingerTracker: ITickable
	{
		private Dictionary<int, Touch> touches;
		private Touch currentTouch;
		private Vector3 currentPosition;

		public FingerTracker ()
		{
			touches = new Dictionary<int, Touch> ();
		}

		public void Tick ()
        {
			touches.Clear ();
			#if UNITY_EDITOR
			touches.Add (-1, CreateMockMouseTouch ());
			#else
			for (int i=0; i<Input.touchCount; i++)
			{
				currentTouch = Input.GetTouch (i);
				touches.Add (currentTouch.fingerId, currentTouch);
			}
			#endif
        }

		#if UNITY_EDITOR
		private Touch CreateMockMouseTouch()
        {
            Touch mock = new Touch ();
			mock.fingerId = -1;
			mock.position = Input.mousePosition;
			return mock;
        }
		#endif

        public bool TryGetWorldPosition (int fingerId, out Vector3? position)
		{
			//Debug.Log (fingerId);
			bool isTouching = touches.TryGetValue (fingerId, out currentTouch);
			if (isTouching)
				Debug.Log (string.Format ("TryGetPosition: {0} - {1}", fingerId, currentTouch.fingerId));
			position = isTouching? ScreenToWorldPosition (currentTouch.position) : null;
			return isTouching;
		}

		private Vector3? ScreenToWorldPosition (Vector2 screenPosition)
		{
			currentPosition = Camera.main.ScreenToWorldPoint (screenPosition);
			currentPosition.z = 0;
			return currentPosition;
		}
    }
}
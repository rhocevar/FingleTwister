using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Power
{
    [RequireComponent (typeof (Collider2D))]
    public class Switche : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
    {
        [Inject] private PowerController powerController;
        private Animator animator;
        private bool isOn;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start ()
        {
            powerController.AddSwitche ();
        }

        #if UNITY_EDITOR
        private void Update ()
        {
            if (Input.GetKeyDown (KeyCode.Space))
                TryTurnOn ();
            else if (Input.GetKeyUp (KeyCode.Space))
                TryTurnOff ();            
        }
        #endif

        public void OnPointerDown(PointerEventData eventData)
        {
            TryTurnOn ();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TryTurnOff ();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TryTurnOff ();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TryTurnOn ();
        }

        private void TryTurnOn ()
        {
            if (isOn) return;
            isOn = true;



            powerController.TurnOnSwitche ();
        }

        private void TryTurnOff ()
        {
            if (!isOn) return;
            isOn = false;

            //Button released animation

            powerController.TurnOffSwitche ();
        }
    }
}
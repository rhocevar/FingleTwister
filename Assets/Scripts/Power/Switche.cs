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
        [Inject]
        private PowerController powerController;
        private bool isOn;
        
        private void Start ()
        {
            powerController.AddSwitche ();
        }

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
            GetComponent<Renderer> ().material.color = Color.green;
            powerController.TurnOnSwitche ();
        }

        private void TryTurnOff ()
        {
            if (!isOn) return;
            isOn = false;
            GetComponent<Renderer> ().material.color = Color.white;
            powerController.TurnOffSwitche ();
        }
    }
}
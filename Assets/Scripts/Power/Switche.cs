using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Power
{
    [RequireComponent (typeof (Collider2D))]
    public class Switche : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
    {
        [Inject] private PowerController powerController;
        [Inject] private AudioManager audioManager;
        private Animator animator;
        private AudioSource audioSource;
        private bool isOn;

        private void Awake()
        {
            powerController.AddSwitche();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
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

            audioSource.clip = audioManager.Audios.SwitchOn;
            audioSource.Play();

            powerController.TurnOnSwitche ();
        }

        private void TryTurnOff ()
        {
            if (!isOn) return;
            isOn = false;

            audioSource.clip = audioManager.Audios.SwitchOff;
            audioSource.Play();

            powerController.TurnOffSwitche ();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioAssets audios;
        public AudioAssets Audios { get { return audios; } }

        private AudioSource audioSource;

        private void Awake()
        {
            gameObject.AddComponent<AudioSource>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            audioSource.clip = audios.PianoBackground;
            audioSource.loop = true;
            audioSource.Play();
        }

    }
}
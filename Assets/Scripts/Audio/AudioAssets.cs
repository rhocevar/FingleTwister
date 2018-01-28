using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioSO", menuName = "Audio/AudioSO", order = 1)]
    public class AudioAssets : ScriptableObject
    {
        public AudioClip SwitchOn;
        public AudioClip SwitchOff;
        public AudioClip Uploading;
        public AudioClip ComputerBreak;
        public AudioClip Steady_hum;
        public AudioClip PianoBackground;
    }
}



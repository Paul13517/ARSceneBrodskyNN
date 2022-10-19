using System;
using UnityEngine;

namespace Immerse.AR
{
    [RequireComponent(typeof(AudioSource))]
    public class ARChapter : MonoBehaviour
    {
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void Setup(AudioClip audio)
        {
            _audioSource.clip = audio;
        }

        public void Play()
        {
            _audioSource.Play();
        }
    }
}
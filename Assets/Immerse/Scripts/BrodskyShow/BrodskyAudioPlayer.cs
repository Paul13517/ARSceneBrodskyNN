using UnityEngine;

namespace Immerse.Brodsky
{
    [RequireComponent(typeof(AudioSource))]
    public class BrodskyAudioPlayer : MonoBehaviour
    {
        private static BrodskyAudioPlayer _instance;
        public static BrodskyAudioPlayer Instance => _instance ??= FindObjectOfType<BrodskyAudioPlayer>();
        
        private AudioSource _audioSource;

        public float CurrentTime => _audioSource.time;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetupAndPlay(AudioClip clip, float time = 0)
        {
            Stop();
            if (clip == null)
            {
                return;
            }
            
            _audioSource.clip = clip;
            _audioSource.time = time;
            Play();
        }

        public void StopAndReset()
        {
            Stop();
            _audioSource.clip = null;
        }

        private void Play() => _audioSource.Play();
        
        public void Pause() => _audioSource.Pause();

        public void UnPause() => _audioSource.UnPause();

        private void Stop() => _audioSource.Stop();
    }
}
using UnityEngine;

namespace Immerse.Brodsky
{
    [RequireComponent(typeof(AudioSource))]
    public class Blufixer : MonoBehaviour
    {
        private AudioSource _audioSource;
        private bool _isPlaying;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (_isPlaying != _audioSource.isPlaying)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }

            _isPlaying = _audioSource.isPlaying;
        }
    }
}

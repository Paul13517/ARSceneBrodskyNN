using System;
using System.Threading;
using System.Threading.Tasks;
using Immerse.Core.UI;
using UnityEngine;

namespace Immerse.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class ImmerseAudioPlayer : MonoBehaviour
    {
        [SerializeField] private TimedProgressBar _timedProgressBar;

        private AudioSource _audioSource;
        private AudioSource AudioSource => _audioSource ??= GetComponentInChildren<AudioSource>(true);

        public bool IsPlaying => AudioSource.isPlaying;


        public void Enable(bool isEnabled)
        {
            gameObject.SetActive(isEnabled);
            if (_timedProgressBar)
            {
                _timedProgressBar.gameObject.SetActive(isEnabled);
            }
        }

        private void OnDisable()
        {
            Stop();
        }

        public async Task Play(AudioClip clip)
        {
            if (AudioSource.clip == clip)
            {
                UnPause();
            }
            else
            {
                Show();
                Stop();
                AudioSource.clip = clip;
                AudioSource.Play();
            }

            if (_timedProgressBar)
            {
                _timedProgressBar.UpdateProgress(AudioSource);
            }

            await Task.Delay(TimeSpan.FromSeconds(clip.length - AudioSource.time));
        }

        public async Task Play(AudioClip clip, CancellationToken cancellationToken)
        {
            Play(clip);
            await Task.Delay(TimeSpan.FromSeconds(clip.length - AudioSource.time), cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                Debug.LogError($"cancel audio");
                Stop();
            }
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Stop()
        {
            AudioSource.Stop();
            AudioSource.clip = null;
        }

        public void Pause()
        {
            AudioSource.Pause();

            if (!_timedProgressBar)
            {
                return;
            }

            _timedProgressBar.Cancel();
        }

        public void UnPause()
        {
            AudioSource.UnPause();

            if (!_timedProgressBar)
            {
                return;
            }

            _timedProgressBar.UpdateProgress(AudioSource);
        }

        public void Mute(bool isMuted)
        {
            AudioSource.mute = isMuted;
        }
    }
}
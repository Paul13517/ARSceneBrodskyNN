using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Immerse.Core.UI
{
    public class TimedProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private RectTransform _fillIndicator;

        [SerializeField] private TMP_Text _timePassed;
        [SerializeField] private TMP_Text _timeLeft;

        private CancellationTokenSource _tokenSource;


        private void OnDisable()
        {
            Reset();
        }

        public void Reset()
        {
            Cancel();

            _timePassed.text = "00:00";
            _timeLeft.text = "-00:00";
            _slider.normalizedValue = 0;
            _fillIndicator.localScale = new Vector3(0, 1, 1);
        }

        public void Cancel()
        {
            if (_tokenSource == null || _tokenSource.IsCancellationRequested)
            {
                return;
            }

            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        }

        public async Task UpdateProgress(VideoPlayer videoPlayer)
        {
            Reset();

            _tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _tokenSource.Token;

            cancellationToken.ThrowIfCancellationRequested();

            while (videoPlayer.time < videoPlayer.length)
            {
                await Task.Delay(TimeSpan.FromSeconds(.1f), cancellationToken);
                
                _timePassed.text = videoPlayer.time.ToHumanTime();
                _timeLeft.text = $"-{(videoPlayer.length - videoPlayer.time).ToHumanTime()}";
                var progress = (float) (videoPlayer.time / videoPlayer.length);
                _slider.normalizedValue = progress > 0 ? progress : 1;
                _fillIndicator.localScale = new Vector3(_slider.normalizedValue, 1, 1);

                if (cancellationToken.IsCancellationRequested)
                {
                    Reset();
                }
            }
        }

        public async Task UpdateProgress(AudioSource audioSource)
        {
            Reset();

            _tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _tokenSource.Token;

            cancellationToken.ThrowIfCancellationRequested();

            while (audioSource.time < audioSource.clip.length)
            {
                await Task.Delay(TimeSpan.FromSeconds(.1f), cancellationToken);
                
                _timePassed.text = audioSource.time.ToHumanTime();
                _timeLeft.text = $"-{(audioSource.clip.length - audioSource.time).ToHumanTime()}";
                var progress = audioSource.time / audioSource.clip.length;
                _slider.normalizedValue = progress > 0 ? progress : 1;
                _fillIndicator.localScale = new Vector3(_slider.normalizedValue, 1, 1);

                if (cancellationToken.IsCancellationRequested)
                {
                    Reset();
                }
            }
        }
    }
}
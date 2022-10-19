using System;
using System.Threading;
using System.Threading.Tasks;
using Immerse.Core.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Immerse.Core
{
    [RequireComponent(typeof(VideoPlayer))]
    public class ImmerseVideoPlayer : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;
        [SerializeField] private TimedProgressBar _timedProgressBar;

        private VideoPlayer _videoPlayer;
        private VideoPlayer VideoPlayer => _videoPlayer ??= GetComponentInChildren<VideoPlayer>(true);

        private Material _renderMaterial;
        private Material RenderMaterial => _renderMaterial ??= _rawImage.material;

        private Action _onFinishedCallback;

        private CancellationTokenSource _tokenSource;
        public bool IsPlaying => _videoPlayer.isPlaying;


        public void Enable(bool isEnabled)
        {
            gameObject.SetActive(isEnabled);
            if (_timedProgressBar)
            {
                _timedProgressBar.gameObject.SetActive(isEnabled);
            }
        }
        private void OnEnable()
        {
            _rawImage.enabled = false;
        }

        private void OnDisable()
        {
            Stop();
        }

        public async Task Play(VideoClip clip)
        {
            if (VideoPlayer.clip == clip)
            {
                Play();
            }
            else
            {
                _rawImage.enabled = false;
                if (_timedProgressBar)
                {
                    _timedProgressBar.gameObject.SetActive(false);
                }
                
                Stop();
                VideoPlayer.clip = clip;
                VideoPlayer.Prepare();

                VideoPlayer.prepareCompleted += _ =>
                {
                    SetupPlayer();
                    Play();
                    
                };
            }

            while (_videoPlayer.time < clip.length - .1f)
            {
                await Task.Delay(TimeSpan.FromSeconds(1f));
            }
        }
        
        public async Task Play(VideoClip clip, CancellationToken cancellationToken)
        {
            Play(clip);
            
            while (_videoPlayer.time < clip.length - .1f)
            {
                await Task.Delay(TimeSpan.FromSeconds(1f), cancellationToken);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                Stop();
            }
        }
        
        private void SetupPlayer()
        {
            var width = (int) VideoPlayer.width;
            var height = (int) VideoPlayer.height;
            var renderTexture = new RenderTexture(width, height, 16);

            RenderMaterial.mainTexture = renderTexture;
            VideoPlayer.targetTexture = renderTexture;
            _rawImage.texture = renderTexture;
            //_rawImage.rectTransform.ResizeToTexture(renderTexture);
            
            _rawImage.enabled = true;
            if (_timedProgressBar)
            {
                _timedProgressBar.gameObject.SetActive(true);
            }
        }
        
        public void Stop()
        {
            VideoPlayer.Stop();
            VideoPlayer.clip = null;
            _rawImage.enabled = false;
            
            VideoPlayer.loopPointReached -= OnLoopPointReached;
        }

        public void Pause()
        {
            VideoPlayer.Pause();

            if (_timedProgressBar)
            {
                _timedProgressBar.Cancel();
            }
        }

        public void Play()
        {
            VideoPlayer.Play();

            if (_timedProgressBar)
            {
                _timedProgressBar.UpdateProgress(VideoPlayer);
            }
        }

        private void OnLoopPointReached(VideoPlayer player)
        {
            _onFinishedCallback?.Invoke();
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Immerse.Core.UI
{
    public class LoaderAnime : MonoBehaviour
    {
        [SerializeField] private Transform _loader;
        [SerializeField] private int _loaderSegments = 12;
        [SerializeField] private float _timeStep = .5f;

        private Coroutine _animeCoroutine;
        private WaitForSeconds _waitForSeconds;


        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_timeStep);
        }

        private void OnEnable()
        {
            if (!_loader)
            {
                return;
            }

            _animeCoroutine = StartCoroutine(Animate());
        }


        private void OnDisable()
        {
            Stop();
        }

        public void Stop()
        {
            if (_animeCoroutine == null)
            {
                return;
            }

            StopCoroutine(_animeCoroutine);
            _animeCoroutine = null;
        }

        private IEnumerator Animate()
        {
            while (true)
            {
                _loader.Rotate(new Vector3(0, 0, -360 / _loaderSegments));
                yield return _waitForSeconds;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    public class ZoomableUIElement : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scroll;
        [SerializeField] private float _speed = 4f;
        [SerializeField] private float _zoomMinScale = 1;
        [SerializeField] private float _zoomMaxScale = 5;

        private float _previousDistance;


        private void OnDisable()
        {
            transform.localScale = Vector3.one;
        }

        private void Update()
        {
            int touchCount = Input.touchCount;

            if (_scroll)
            {
                _scroll.horizontal = touchCount != 2;
                _scroll.vertical = touchCount != 2;
            }

            if (touchCount != 2)
            {
                return;
            }

            float distance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

            if (distance < _previousDistance && transform.localScale.x > _zoomMinScale)
            {
                Vector3 targetScale = transform.localScale -= .1f * Vector3.one;
                transform.localScale = Vector3.Slerp(transform.localScale, targetScale, _speed * Time.deltaTime);
            }
            else if (distance > _previousDistance && transform.localScale.x < _zoomMaxScale)
            {
                Vector3 targetScale = transform.localScale += .1f * Vector3.one;
                transform.localScale = Vector3.Slerp(transform.localScale, targetScale, _speed * Time.deltaTime);
            }

            _previousDistance = distance;
        }
    }
}
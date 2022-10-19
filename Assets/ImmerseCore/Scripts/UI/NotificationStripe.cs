using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    public class NotificationStripe : MonoBehaviour
    {
        private static NotificationStripe _instance;
        public static NotificationStripe Instance => _instance ??= FindObjectOfType<NotificationStripe>(true);
        
        
        [Space, SerializeField] private float _closedOffset = -150f;
        [SerializeField] private float _showDuration = 3f;
        [SerializeField] private float _transitionDuration = .3f;

        private TMP_Text _text;
        private RectTransform _rectTransform;
        private Vector2 _openedPosition;
        private Vector2 _closedPosition;


        public void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>(true);
            _rectTransform = GetComponentInChildren<Image>(true).rectTransform;

            _openedPosition = _rectTransform.anchoredPosition;
            _closedPosition = _openedPosition - new Vector2(0, _closedOffset);

            LeanTween.move(_rectTransform, _closedPosition, 0);
            LeanTween.scale(_rectTransform, Vector3.one * .5f, 0);
            LeanTween.alpha(_rectTransform, 0, 0);

            _rectTransform.gameObject.SetActive(false);
        }

        public void ShowNotification(string message)
        {
            _text.text = message;
            Show();
        }

        private void Show()
        {
            _rectTransform.gameObject.SetActive(true);

            LeanTween.move(_rectTransform, _openedPosition, _transitionDuration);
            LeanTween.scale(_rectTransform, Vector3.one, _transitionDuration);
            LeanTween.alpha(_rectTransform, 1, _transitionDuration).setOnComplete(() =>
            {
                LeanTween.delayedCall(_showDuration, () =>
                {
                    LeanTween.move(_rectTransform, _closedPosition, _transitionDuration);
                    LeanTween.scale(_rectTransform, Vector3.one * .5f, _transitionDuration);
                    LeanTween.alpha(_rectTransform, 0, _transitionDuration).setOnComplete(() => { _rectTransform.gameObject.SetActive(false); });
                });
            });
        }
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    [RequireComponent(typeof(Button))]
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;
        [Header("Optional")]
        [Space,SerializeField] private Image _toggleImage;
        public Image ToggleImage => _toggleImage ??= _button.image;
        
        private Button _button;
        private Button Button => _button ??= GetComponent<Button>();
        
        private bool _isActive = true;
        private bool _reactivateOnReset;


        private void Awake()
        {
            ToggleImage.sprite = _activeSprite;
            Button.onClick.AddListener(Toggle);
        }

        public void AddListener(UnityAction callback)
        {
            Button.onClick.AddListener(callback);
        }
        
        public void RemoveListener(UnityAction callback)
        {
            Button.onClick.RemoveListener(callback);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _reactivateOnReset = true;
        }
        
        public void Reset()
        {
            if (_reactivateOnReset)
            {
                gameObject.SetActive(true);
                _reactivateOnReset = false;
            }
            
            _isActive = true;
            ToggleImage.sprite = _activeSprite;
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            ToggleImage.sprite = _isActive ? _activeSprite : _inactiveSprite;
        }
        
        public void Toggle()
        {
            _isActive = !_isActive;
            ToggleImage.sprite = _isActive ? _activeSprite : _inactiveSprite;
        }
    }
}

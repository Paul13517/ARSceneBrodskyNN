using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    [RequireComponent(typeof(Button))]
    public class ImmerseButton : MonoBehaviour
    {
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private Color _enabledColor;
        [SerializeField] private Color _disabledColor;

        private Button _button;
        private TMP_Text _text;

        
        public void Init(UnityAction callback)
        {
            _button = GetComponentInChildren<Button>(true);
            _text = GetComponentInChildren<TMP_Text>(true);
            _button.onClick.AddListener(callback);
        }

        public void SetText(string text)
        {
            if (!_text)
            {
                return;
            }
            
            _text.text = text;
        }

        public void SetEnabled(bool isEnabled)
        {
            _button.interactable = isEnabled;
            if (_disabledSprite)
            {
                _button.image.sprite = isEnabled ? _enabledSprite : _disabledSprite;
            }

            if (_text)
            {
                _text.color = isEnabled ? _enabledColor : _disabledColor;
            }
        }
    }
}
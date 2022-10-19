using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderButton : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _handle;
        [SerializeField] private TMP_Text _text;

        [Space, SerializeField] private Sprite _enabledBackground;
        [SerializeField] private Sprite _disabledBackground;
        
        [Space, SerializeField] private Sprite _enabledHandle;
        [SerializeField] private Sprite _disabledHandle;
        
        [Space, SerializeField] private Color _enabledColor;
        [SerializeField] private Color _disabledColor;

        private bool _isOn;


        public void Init(Action callback)
        {
            if (_text)
            {
                _enabledColor = _text.color;
            }

            _slider.onValueChanged.AddListener(value =>
            {
                if (_isOn || value < .9f)
                {
                    return;
                }
                
                _isOn = true;
                _slider.value = 1;
                callback?.Invoke();
            });
        }

        private void OnDisable()
        {
            Reset();
        }

        public void Reset()
        {
            _slider.value = 0;
            _isOn = false;
        }

        public void SetEnabled(bool isEnabled)
        {
            _slider.interactable = isEnabled;

            _slider.image.sprite = isEnabled ? _enabledBackground : _disabledBackground;
            _handle.sprite = isEnabled ? _enabledHandle : _disabledHandle;
            _text.color = isEnabled ? _enabledColor : _disabledColor;
        }
    }
}
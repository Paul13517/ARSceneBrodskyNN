using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FontStyles = TMPro.FontStyles;

namespace Immerse.Core.UI
{
    [RequireComponent(typeof(Button))]
    public class PopUpButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _primaryChoiseColor;
        [SerializeField] private Color _secondaryChoiseColor;

        public virtual void Setup(PopUpButtonOptions options, Action callback)
        {
            _text.text = options.text;
            if (_primaryChoiseColor.Equals(_secondaryChoiseColor))
            {
                _text.fontStyle = options.preferedChoise ? FontStyles.Bold : FontStyles.Normal;
            }
            else
            {
                _text.color = options.preferedChoise ? _primaryChoiseColor : _secondaryChoiseColor;
            }
            
            GetComponent<Button>().onClick.AddListener(() =>
            {
                callback?.Invoke();
                options.callback?.Invoke();
            });
        }
    }
}
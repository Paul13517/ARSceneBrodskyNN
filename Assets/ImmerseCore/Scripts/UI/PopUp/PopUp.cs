using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    public class PopUp : MonoBehaviour
    {
        private static PopUp _instance;
        public static PopUp Instance => _instance ??= FindObjectOfType<PopUp>(true);

        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _message;

        [Space, SerializeField] private int _maxButtonSymbolsInHorizontalLayout;
        [SerializeField] private Transform _horizontalLayoutGroup;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

        [Space, SerializeField] private GameObject _popUpButtonPrefab;

        private readonly List<PopUpButton> _buttons = new();


        public async Task Show(PopUpOptions options)
        {
            await ClearButtons();

            // workaround
            if (string.IsNullOrEmpty(options.header))
            {
                _message.rectTransform.offsetMax = new Vector2(0, 150);
            }
            
            _header.text = options.header;
            _message.text = options.message;
            _message.gameObject.SetActive(options.message != null);
            
            foreach (PopUpButtonOptions buttonOption in options.buttonsOptions)
            {
                Transform buttonsParent = GetButtonsParent(options.buttonsOptions);
                var button = Instantiate(_popUpButtonPrefab, buttonsParent).GetComponent<PopUpButton>();
                button.Setup(buttonOption, Hide);
                _buttons.Add(button);
            }

            ResizePopUp();
            _panel.SetActive(true);
        }

        private Transform GetButtonsParent(List<PopUpButtonOptions> buttonOptions)
        {
            if (buttonOptions.Count == 2 &&
                buttonOptions.All(x => x.text.Length <= _maxButtonSymbolsInHorizontalLayout))
            {
                _horizontalLayoutGroup.gameObject.SetActive(true);
                _verticalLayoutGroup.gameObject.SetActive(false);

                return _horizontalLayoutGroup;
            }

            _horizontalLayoutGroup.gameObject.SetActive(false);
            _verticalLayoutGroup.gameObject.SetActive(true);

            return _verticalLayoutGroup.transform;
        }

        private void ResizePopUp()
        {
            var buttons = _buttons.Select(x=>x.GetComponent<LayoutElement>());
            float minHeight = buttons.Sum(x => x.minHeight) + buttons.Count() * _verticalLayoutGroup.spacing;
            _verticalLayoutGroup.GetComponent<LayoutElement>().minHeight = minHeight;
        }

        private void Hide()
        {
            _panel.SetActive(false);
        }

        private async Task ClearButtons()
        {
            foreach (PopUpButton button in _buttons)
            {
                Destroy(button.gameObject);
            }

            _buttons.Clear();
            await Task.Yield();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    public class DiscreteProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject _progressPartPrefab;
        [SerializeField] private Color _selectedColor = Color.black;

        private List<Image> _progressParts = new();
        private Color _defaultColor;

        public void Init(int progressParts)
        {
            Clear();
            SetActive(progressParts);

            if (progressParts <= 1)
            {
                return;
            }

            for (int i = 0; i < progressParts; i++)
            {
                _progressParts.Add(Instantiate(_progressPartPrefab, transform).GetComponent<Image>());
            }

            _defaultColor = _progressParts[0].color;
        }

        public void UpdateProgress(int currentProgress)
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            for (int i = 0; i < _progressParts.Count; i++)
            {
                _progressParts[i].color = i < currentProgress ? _selectedColor : _defaultColor;
            }
        }

        private void SetActive(int progressParts)
        {
            var canvasGroup = GetComponentInChildren<CanvasGroup>(true);
            if (canvasGroup)
            {
                canvasGroup.alpha = progressParts > 1 ? 1 : 0;
            }
            else
            {
                gameObject.SetActive(progressParts > 1);
            }
        }

        private void Clear()
        {
            foreach (Image progressPart in _progressParts)
            {
                Destroy(progressPart.gameObject);
            }

            _progressParts.Clear();
        }
    }
}
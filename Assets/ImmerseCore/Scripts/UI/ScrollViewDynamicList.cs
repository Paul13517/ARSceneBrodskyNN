using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollViewDynamicList : MonoBehaviour
    {
        private ScrollRect _scrollRect;
        private Transform _content;
        
        private readonly List<RectTransform> _scrollItems = new();

        private bool _scrollAlwaysEnabled;
        private float _scrollHeight;
        
        
        public void Init(bool scrollAlwaysEnabled = false)
        {
            _scrollAlwaysEnabled = scrollAlwaysEnabled;
            _scrollRect = GetComponent<ScrollRect>();
            _content = _scrollRect.content;
            _scrollHeight = _scrollRect.GetComponent<RectTransform>().rect.height;
            
            Clear();
        }

        private void OnEnable()
        {
            SetScrollEnabled();
        }

        private void OnDisable()
        {
            _scrollRect.content.anchoredPosition = Vector2.zero;
        }

        public GameObject Add(GameObject prefab)
        {
            GameObject scrollItem = Instantiate(prefab, _content);
            var scrollItemRect = scrollItem.GetComponent<RectTransform>();
            _scrollItems.Add(scrollItemRect);

            SetScrollEnabled();

            return scrollItem;
        }

        public void Clear()
        {
            foreach (var scrollItem in _scrollItems)
            {
                Destroy(scrollItem.gameObject);
            }

            _scrollItems.Clear();
        }

        private async Task SetScrollEnabled()
        {
            if (_scrollAlwaysEnabled)
            {
                _scrollRect.vertical = true;
                return;
            }
            
            await Task.Delay(100);
            float contentHeight = _scrollRect.content.rect.height;
            _scrollRect.vertical = contentHeight > _scrollHeight;
        }
    }
}
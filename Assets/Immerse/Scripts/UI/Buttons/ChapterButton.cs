using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    [RequireComponent(typeof(Button))]
    public class ChapterButton : MonoBehaviour
    {
        public Action<Chapter> OnClick;

        [SerializeField] private TMP_Text _text;
        
        
        public void Init(Chapter chapter)
        {
            Button button = GetComponent<Button>();
            _text.text = chapter.Name;
            button.onClick.AddListener(() =>
            {
                OnClick?.Invoke(chapter);
            });
        }
    }
}
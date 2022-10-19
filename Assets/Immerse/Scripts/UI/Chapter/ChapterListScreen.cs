using System.Collections.Generic;
using Immerse.Brodsky.PUN;
using Immerse.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    [RequireComponent(typeof(SliderScreen))]
    public class ChapterListScreen : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private ScrollViewDynamicList _scrollView;
        [SerializeField] private GameObject _chapterButtonPrefab;

        private SliderScreen _sliderScreen;
        private SliderScreen SliderScreen => _sliderScreen ??= GetComponentInChildren<SliderScreen>(true);

        
        public void Open()
        {
            SliderScreen.Open();
        }

        private void Close()
        {
            SliderScreen.Close();
        }

        public void Init(List<Chapter> chapters)
        {
            SliderScreen.Init(BrodskySettings.ScreenAnimationDuration, ScreenPosition.Left, ScreenPosition.Left);
            _scrollView.Init();
            _backButton.onClick.AddListener(Close);
            _scrollView.Clear();
            Close();
            
            foreach (Chapter chapter in chapters)
            {
                ChapterButton chapterButton = _scrollView.Add(_chapterButtonPrefab).GetComponent<ChapterButton>();
                chapterButton.Init(chapter);
                chapterButton.OnClick += OnChapterSelected;
            }
        }

        private void OnChapterSelected(Chapter chapter)
        {
            BrodskyPopUps.ShowChapterChangePopUp(chapter, () =>
            {
                MultiplayerEvents.FinishChapter(chapter);
                Close();
            });
        }
    }
}
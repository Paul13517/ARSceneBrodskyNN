using Immerse.Brodsky.PUN;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class ChapterScreen : BrodskyScreen
    {
        [SerializeField] private ChapterHeaderPanel _headerPanel;
        [SerializeField] private CommentsPanel _commentsPanel;
        [SerializeField] private ChapterTimePanel _timePanel;
        [SerializeField] private ChapterButtonsPanel _buttonsPanel;
        
        [Space, SerializeField] private Button _chapterListButton;
        [SerializeField] private ChapterListScreen _chapterListScreen;
        
        [Space, SerializeField] private ARScreen _arScreen;
        
        private Chapter _currentChapter;
        
        
        public override void Init()
        {
            _buttonsPanel.Init(RunAR, () => _timePanel.TimeLeft > 0);

            _chapterListButton.onClick.AddListener(_chapterListScreen.Open);
          
            _arScreen.Init(BrodskyShow.Chapters.Count);
            _headerPanel.Init(BrodskyShow.Chapters.Count);
            _chapterListScreen.Init(BrodskyShow.Chapters);
            
            BrodskyEvents.RoleChosen += OnRoleChosen;
            BrodskyEvents.ChapterChanged += OnChapterChanged;
            BrodskyEvents.SwitchedToAR += OnSwitchedToAR;
            BrodskyEvents.ShowFinished += OnShowFinished;
            OnChapterChanged(BrodskyShow.Chapters[0]);
        }

        private void OnRoleChosen(bool isMaster)
        {
            _commentsPanel.RoleChosen(isMaster);
            _headerPanel.gameObject.SetActive(isMaster);
            _timePanel.gameObject.SetActive(isMaster);
        }

        private void OnChapterChanged(Chapter chapter)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }
            
            _currentChapter = chapter;
            
            _buttonsPanel.Setup(chapter.HasAR, chapter.SortingOrder == BrodskyShow.Chapters.Count);
            BrodskyAudioPlayer.Instance.SetupAndPlay(chapter.audio, chapter.StartTime);
            chapter.Run(_commentsPanel.SetComment,_commentsPanel.RemoveComment, OnArAvailable);

            if (!BrodskySettings.IsMaster)
            {
                return;
            }
            
            _headerPanel.Setup(chapter);
            if (!chapter.HasAR || chapter.StartTime < chapter.ArAvailableAt)
            {
                _timePanel.Setup(chapter.Duration - chapter.StartTime, false);
            }
        }
        
        private void OnSwitchedToAR(bool isEnabled)
        {
            gameObject.SetActive(!isEnabled);
        }

        private void OnShowFinished()
        {
            BrodskyAudioPlayer.Instance.StopAndReset();
            Exit();
        }

        private void OnArAvailable()
        {
            _buttonsPanel.SetArButtonsEnabled(true);
            MultiplayerEvents.Vibrate();

            if (!BrodskySettings.IsMaster)
            {
                return;
            }

            float timeLeft = _currentChapter.ArDuration - _currentChapter.StartTime;
            if (_currentChapter.StartTime > _currentChapter.ArAvailableAt)
            {
                timeLeft += _currentChapter.ArAvailableAt;
            }

            _timePanel.Setup(timeLeft, true);
        }

        private void RunAR()
        {
            _arScreen.Show();
            _arScreen.Setup(_currentChapter, _timePanel.TimeLeft);
        }
    }
}
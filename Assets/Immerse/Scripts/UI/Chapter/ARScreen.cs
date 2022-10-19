using System.Collections;
using Immerse.Brodsky.PUN;
using Immerse.Core;
using Immerse.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class ARScreen : MonoBehaviour
    {
        [SerializeField] private ChapterHeaderPanel _headerPanel;
        [SerializeField] private ChapterTimePanel _timePanel;
        [SerializeField] private SliderButton _finishAR;

        [Space, SerializeField] private GameObject _screenRecordingPanel;
        [SerializeField] private TMP_Text _screenRecordingText;
        [SerializeField] private Button _infoButton;
        
        [Space, SerializeField] private GameObject _instructionPanel;
        [SerializeField] private Button _closeInstructionButton;

        private Coroutine _recordingCoroutine;


        public void Init(int chaptersCount)
        {
            _headerPanel.Init(chaptersCount);
            _finishAR.Init(() => BrodskyPopUps.ShowChapterNotOver(
                () => MultiplayerEvents.FinishChapter(),
                ()=> _finishAR.Reset(),
                () => _timePanel.TimeLeft > 0));

            BrodskyEvents.ChapterFinished += nextChapterIndex => Hide();
            BrodskyEvents.RoleChosen += OnRoleChosen;

            _instructionPanel.SetActive(false);
            _infoButton.onClick.AddListener(() => { _instructionPanel.SetActive(true); });
            _closeInstructionButton.onClick.AddListener(() => { _instructionPanel.SetActive(false); });
        }


        private void OnEnable()
        {
            ScreenRecorder.RecordingStateChanged += OnRecordingStateChanged;
            ScreenRecorder.VideoSaved += OnVideoSaved;
        }

        private void OnDisable()
        {
            ScreenRecorder.RecordingStateChanged -= OnRecordingStateChanged;
            ScreenRecorder.VideoSaved -= OnVideoSaved;
        }

        public void Setup(Chapter chapter, float timeLeft)
        {
            chapter.IsRunningAR = true;
            BrodskyAudioPlayer.Instance.SetupAndPlay(chapter.audioAR);

            if (!BrodskySettings.IsMaster)
            {
                return;
            }

            _headerPanel.Setup(chapter);
            _timePanel.Setup(timeLeft, true);
        }

        public void Show()
        {
            BrodskyEvents.OnSwitchedToAR(true);
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            BrodskyEvents.OnSwitchedToAR(false);
            gameObject.SetActive(false);
            ScreenRecorder.StopRecording();
        }

        private void OnRoleChosen(bool isMaster)
        {
            _headerPanel.gameObject.SetActive(isMaster);
            _timePanel.gameObject.SetActive(isMaster);
            _finishAR.gameObject.SetActive(isMaster);
            _screenRecordingPanel.SetActive(!isMaster);
        }

        private void OnVideoSaved(bool hasError)
        {
            NotificationStripe.Instance.ShowNotification(hasError ? BrodskyUIStrings.VideoNotSaved : BrodskyUIStrings.VideoSaved);
        }

        private void OnRecordingStateChanged(bool isRecording)
        {
            if (isRecording)
            {
                _recordingCoroutine = StartCoroutine(UpdateRecordingText());
            }
            else
            {
                StopCoroutine(_recordingCoroutine);
                _recordingCoroutine = null;
                _screenRecordingText.text = "00:00";
            }
        }

        private IEnumerator UpdateRecordingText()
        {
            var wait = new WaitForSeconds(1);
            float recordingTime = 0;
            while (true)
            {
                yield return wait;
                recordingTime++;
                _screenRecordingText.text = recordingTime.ToHumanTime();
            }
        }
    }
}
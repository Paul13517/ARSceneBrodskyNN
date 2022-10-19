using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    [RequireComponent(typeof(Button))]
    public class ScreenRecordingButton : MonoBehaviour
    {
        [SerializeField] private Sprite _startRecordingSprite;
        [SerializeField] private Sprite _stopRecordingSprite;

        private Button _button;
        private bool _isRecording;
        
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.image.sprite = _startRecordingSprite;
            _button.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            ScreenRecorder.RecordingStateChanged += OnRecordingStateChanged;
        }

        private void OnDisable()
        {
            ScreenRecorder.RecordingStateChanged -= OnRecordingStateChanged;
        }

        private void OnClick()
        {
            ScreenRecorder.ToggleRecording();
            _isRecording = !_isRecording;
            _button.image.sprite = _isRecording ? _stopRecordingSprite : _startRecordingSprite;
        }

        private void OnRecordingStateChanged(bool isRecording)
        {
            if (!isRecording && _isRecording)
            {
                _isRecording = false;
                _button.image.sprite = _startRecordingSprite;
            }
        }
    }
}
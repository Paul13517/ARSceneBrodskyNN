using Immerse.Core;
using Immerse.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class PermissionScreen : BrodskyScreen
    {
        [SerializeField] private Toggle _microphonePermission;
        [SerializeField] private Toggle _cameraPermission;
        [SerializeField] private Toggle _screenRecordingPermission;

        [Space, SerializeField] private Button _nextButton;

        [Space, SerializeField] private GameObject _permissionsNotGrantedPanel;
        [SerializeField] private Button _allowAllButton;


        private void OnEnable()
        {
            ScreenRecorder.RecordingPrepared += OnRecordingPrepared;
        }

        private void OnDisable()
        {
            ScreenRecorder.RecordingPrepared -= OnRecordingPrepared;
        }

        public override void Init()
        {
            _nextButton.onClick.AddListener(() => BrodskyScreenController.Instance.OpenScreen("LocalizationScreen"));

            _allowAllButton.onClick.AddListener(() =>
            {
                if (!PermissionManager.IsScreenRecordingPermissionGranted())
                {
                    PermissionManager.RequestScreenRecordingPermission();
                }

                if (PermissionManager.IsCameraPermissionGranted() && PermissionManager.IsMicrophonePermissionGranted())
                {
                    return;
                }

#if UNITY_IOS
                Application.OpenURL("app-settings:");
#elif UNITY_ANDROID
                PermissionManager.RequestCameraPermission();
                PermissionManager.RequestMicrophonePermission();
#endif
            });
        }

        protected override void OnOpened()
        {
            ScreenRecorder.Init();

#if UNITY_EDITOR
            _permissionsNotGrantedPanel.SetActive(false);
            _nextButton.gameObject.SetActive(true);

            _cameraPermission.isOn = true;
            _microphonePermission.isOn = true;
            _screenRecordingPermission.isOn = true;
#else
            OnApplicationFocus(true);

            PermissionManager.RequestCameraPermission();
            PermissionManager.RequestMicrophonePermission();
            PermissionManager.RequestScreenRecordingPermission();
#endif
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }

            _cameraPermission.isOn = PermissionManager.IsCameraPermissionGranted();
            _microphonePermission.isOn = PermissionManager.IsMicrophonePermissionGranted();

            bool isRequiredPermissionsGranted = PermissionManager.IsCameraPermissionGranted() &&
                                                PermissionManager.IsMicrophonePermissionGranted();

            _permissionsNotGrantedPanel.SetActive(!isRequiredPermissionsGranted);
            _nextButton.gameObject.SetActive(isRequiredPermissionsGranted);
        }

        private void OnRecordingPrepared(bool isSuccessful)
        {
            //ARLogger.Log($"{isSuccessful}");
            _screenRecordingPermission.isOn = isSuccessful;
        }
    }
}
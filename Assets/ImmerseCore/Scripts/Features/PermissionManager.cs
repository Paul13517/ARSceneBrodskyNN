using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace Immerse.Core
{
    public static class PermissionManager
    {
        public static bool IsCameraPermissionGranted()
        {
#if UNITY_ANDROID
            return Permission.HasUserAuthorizedPermission(Permission.Camera);
#elif UNITY_IOS
            return Application.HasUserAuthorization(UserAuthorization.WebCam);
#else
            return true;
#endif
        }

        public static void RequestCameraPermission()
        {
            if (IsCameraPermissionGranted())
            {
                return;
            }

#if UNITY_ANDROID
            Permission.RequestUserPermission(Permission.Camera);
#elif UNITY_IOS
            Application.RequestUserAuthorization(UserAuthorization.WebCam);
#endif
        }

        public static bool IsMicrophonePermissionGranted()
        {
#if UNITY_ANDROID
            return Permission.HasUserAuthorizedPermission(Permission.Microphone);
#elif UNITY_IOS
            return Application.HasUserAuthorization(UserAuthorization.Microphone);
#else
            return true;
#endif
        }

        public static void RequestMicrophonePermission()
        {
            if (IsMicrophonePermissionGranted())
            {
                return;
            }

#if PLATFORM_ANDROID
            Permission.RequestUserPermission(Permission.Microphone);
#elif UNITY_IOS
            Application.RequestUserAuthorization(UserAuthorization.Microphone);
#endif
        }

        public static bool IsScreenRecordingPermissionGranted()
        {
            return ScreenRecorder.IsPermissionGranted;
        }

        public static void RequestScreenRecordingPermission()
        {
            if (IsScreenRecordingPermissionGranted())
            {
                return;
            }

            ScreenRecorder.PrepareRecording();
        }
    }
}
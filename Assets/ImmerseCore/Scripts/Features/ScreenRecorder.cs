using System;
using VoxelBusters.ReplayKit;

namespace Immerse.Core
{
    public static class ScreenRecorder
    {
        public static Action<bool> RecordingPrepared;
        public static Action<bool> VideoSaved;
        public static Action<bool> RecordingStateChanged;

        private static bool _isRecording;
        private static bool _recordingStoppedByUser;

        public static bool IsPermissionGranted { get; private set; }


        public static void Init()
        {
            ReplayKitManager.DidInitialise             -= OnInitialise;
            ReplayKitManager.DidRecordingStateChange   -= OnRecordingStateChange;

            ReplayKitManager.DidInitialise             += OnInitialise;
            ReplayKitManager.DidRecordingStateChange   += OnRecordingStateChange;

            ReplayKitManager.Initialise();
        }

        public static void ToggleRecording()
        {
            if (ReplayKitManager.IsRecording())
            {
                StopRecording();
            }
            else
            {
                StartRecording();
            }
        }

        private static void StartRecording()
        {
            ReplayKitManager.StartRecording();
        }

        public static void DiscardRecording()
        {
            ReplayKitManager.Discard();
        }

        public static void StopRecording()
        {
            if (!ReplayKitManager.IsRecording())
            {
                return;
            }

            _recordingStoppedByUser = true;
            ReplayKitManager.StopRecording((filePath, error) => 
            {
                _recordingStoppedByUser = false;
                SavePreview();
            });
        }

        private static void SavePreview()
        {
            if(ReplayKitManager.IsPreviewAvailable())
            {
                ReplayKitManager.SavePreview((error) =>
                {
                    VideoSaved?.Invoke(error != null);
                });
            }
            else
            {
                VideoSaved?.Invoke(true);
            }
        }
        
        private static void OnInitialise(ReplayKitInitialisationState state, string message)
        {
            switch (state)
            {
                case ReplayKitInitialisationState.Success:
                    PrepareRecording();
                    break;
                case ReplayKitInitialisationState.Failed:
                    break;
            }
        }

        private static void OnRecordingStateChange(ReplayKitRecordingState state, string message)
        {
            switch(state)
            {
                case ReplayKitRecordingState.Started:
                    _isRecording = true;
                    RecordingStateChanged?.Invoke(_isRecording);
                    break;
                case ReplayKitRecordingState.Stopped:
                    _isRecording = false;
                    IsPermissionGranted = true;
                    RecordingStateChanged?.Invoke(_isRecording);
                    break;
                case ReplayKitRecordingState.Failed:
                    _isRecording = false;
                    RecordingStateChanged?.Invoke(_isRecording);
                    break;
                case ReplayKitRecordingState.Available:
                    if (!_recordingStoppedByUser)
                    {
                        SavePreview();
                    }
                    break;
            }
        }
        
        public static void PrepareRecording()
        {
            ReplayKitManager.SetMicrophoneStatus(true);
            ReplayKitManager.PrepareRecording(error =>
            {
                IsPermissionGranted = error == null;
                RecordingPrepared?.Invoke(IsPermissionGranted);
            });
        }
    }
}
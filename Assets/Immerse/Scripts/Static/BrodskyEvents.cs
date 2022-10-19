using System;
using System.Collections.Generic;
using Immerse.Brodsky.Data;
using Immerse.Brodsky.PUN;
using Immerse.Core.AR;
using UnityEngine;

namespace Immerse.Brodsky
{
    public static class BrodskyEvents
    {
        public static event Action<float> DownloadProgressUpdated;
        public static event Action ContentDownloaded;
        public static event Action<bool> RoleChosen;
        public static event Action MultiplayerConnected;
        public static event Action<int> PlayersUpdated;
        public static event Action ShowStarted;
        public static event Action<Chapter> ChapterChanged;
        public static event Action<bool> SwitchedToAR;
        public static event Action<int> ChapterFinished;
        public static event Action ShowFinished;
        

        public static void OnLocaleSelected(BrodskyLocale locale)
        {
            BrodskySettings.Locale = locale;
            DataLoader.ChapterLoad();
        }

        public static void OnMultiplayerSettingsSetup()
        {
            PhotonConnector.Instance.Connect(BrodskySettings.IsMaster, BrodskySettings.RoomName);
            RoleChosen?.Invoke(BrodskySettings.IsMaster);
        }

        public static void OnMultiplayerConnected()
        {
            MultiplayerConnected?.Invoke();
        }

        public static void OnPlayersUpdated(int players)
        {
            PlayersUpdated?.Invoke(players);
        }

        public static void OnDownloadProgressUpdated(float progress)
        {
            DownloadProgressUpdated?.Invoke(progress);
        }
        
        public static void OnContentDownloaded(List<Chapter> chapters, bool hasError)
        {
            if (hasError)
            {
                //TODO: event on error
                Debug.LogError($"Error content downloading");
                return;
            }

            try
            {
                BrodskyShow.Init(chapters);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            
            ContentDownloaded?.Invoke();
        }
        
        public static void OnShowStarted()
        {
            ShowStarted?.Invoke();
        }

        public static void OnChapterChanged(Chapter chapter)
        {
            ChapterChanged?.Invoke(chapter);
        }

        public static void OnSwitchedToAR(bool isEnabled)
        {
            CameraController.Instance.EnableAR(isEnabled);
            SwitchedToAR?.Invoke(isEnabled);
        }

        public static void OnChapterFinished(int nextChapterIndex)
        {
            ChapterFinished?.Invoke(nextChapterIndex);
        }

        public static void OnShowFinished()
        {
            ShowFinished?.Invoke();
        }
    }
}
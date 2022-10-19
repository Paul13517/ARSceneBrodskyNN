using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Immerse.Brodsky.PUN
{
    public class MultiplayerEvents : MonoBehaviourPun, IOnEventCallback
    {
        private const byte StartShowEventCode = 1;
        private const byte ChapterFinishedEventCode = 2;
        private const byte SendProgressEventCode = 3;

        private static readonly Dictionary<int, EventData> _cachedEvents = new Dictionary<int, EventData>();
        private static readonly HashSet<byte> _eventCodes = new HashSet<byte>()
        {
            StartShowEventCode,
            ChapterFinishedEventCode,
            SendProgressEventCode
        };

        private static readonly RaiseEventOptions _raiseEventOptions = new RaiseEventOptions()
        {
            CachingOption = EventCaching.AddToRoomCache,
            Receivers = ReceiverGroup.Others
        };
        private static int _currentEventIndex = -1;
        
        
        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }


        public static void StartShow()
        {
            if (!BrodskySettings.IsMaster)
            {
                return;
            }

            _currentEventIndex++;
            int eventIndex = _currentEventIndex;
            PhotonNetwork.RaiseEvent(StartShowEventCode,
                new object[] {eventIndex},
                _raiseEventOptions,
                SendOptions.SendReliable);
            
            BrodskyEvents.OnShowStarted();
        }

        public static void FinishChapter(Chapter nextChapter = null)
        {
            if (!BrodskySettings.IsMaster)
            {
                return;
            }
            
            _currentEventIndex++;
            int eventIndex = _currentEventIndex;
            int nextChapterIndex = BrodskyShow.GetChapterIndex(nextChapter);
            PhotonNetwork.RaiseEvent(ChapterFinishedEventCode,
                new object[] {eventIndex, nextChapterIndex},
                _raiseEventOptions,
                SendOptions.SendReliable);
            
            BrodskyEvents.OnChapterFinished(nextChapterIndex);
        }

        public static void SendProgress(int[] targetActors, int chapterIndex, float time)
        {
            _currentEventIndex++;
            int eventIndex = _currentEventIndex;
            
            PhotonNetwork.RaiseEvent(SendProgressEventCode,
                new object[] {eventIndex, chapterIndex, time},
                new RaiseEventOptions() {CachingOption = EventCaching.DoNotCache, TargetActors = targetActors},
                SendOptions.SendReliable);
        }

        public void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;
            if (!_eventCodes.Contains(eventCode))
            {
                return;
            }
            
            object[] data = (object[]) photonEvent.CustomData;
            int eventIndex = (int) data[0];
            if (eventIndex == _currentEventIndex + 1)
            {
                _currentEventIndex++;
                InvokeCallback(photonEvent);

                while (_cachedEvents.ContainsKey(_currentEventIndex + 1))
                {
                    _currentEventIndex++;
                    EventData cachedEvent = _cachedEvents[_currentEventIndex];
                    InvokeCallback(cachedEvent);
                    _cachedEvents.Remove(_currentEventIndex);
                }
                return;
            }
            
            if (eventIndex > _currentEventIndex + 1 && !_cachedEvents.ContainsKey(eventIndex))
            {
                _cachedEvents.Add(eventIndex, photonEvent);
            }
        }

        private void InvokeCallback(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;
            object[] data = (object[]) photonEvent.CustomData;
                
            switch (eventCode)
            {
                case StartShowEventCode:
                    StartShowRPC();
                    return;
                case ChapterFinishedEventCode:
                    var nextChapterIndex = (int) data[1];
                    FinishChapterRPC(nextChapterIndex);
                    return;
                case SendProgressEventCode:
                    var chapterIndex = (int) data[1];
                    var time = (float) data[2];
                    SendProgressRPC(chapterIndex, time);
                    return;
            }
        }

        private static void StartShowRPC()
        {
            Vibrate();
            BrodskyEvents.OnShowStarted();
        }

        private static void FinishChapterRPC(int nextChapterIndex)
        {
            Vibrate();
            BrodskyEvents.OnChapterFinished(nextChapterIndex);
        }

        private static void SendProgressRPC(int chapterIndex, float time)
        {
            BrodskyShow.SetProgress(chapterIndex, time);
        }

        public static void Vibrate()
        {
#if UNITY_ANDROID || UNITY_IOS
                Handheld.Vibrate();
# endif
        }
    }
}
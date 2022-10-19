using System;
using Immerse.Brodsky.PUN;
using Immerse.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class WaitingScreen : BrodskyScreen
    {
        [SerializeField] private GameObject _clientPanel;
        
        [Space, SerializeField] private GameObject _masterPanel;
        [SerializeField] private TMP_Text _viewersConnected;
        [SerializeField] private Button _startButton;
        //[SerializeField] private SliderButton _startButton;

        private int _playersJoinedRoom;
        
        
        public override void Init()
        {
            _startButton.onClick.AddListener(MultiplayerEvents.StartShow);
            _startButton.onClick.AddListener(() => BrodskyScreenController.Instance.OpenScreen("ChapterScreen"));
            BrodskyEvents.RoleChosen += OnRoleChosen;
        }

        private void OnEnable()
        {
            BrodskyEvents.PlayersUpdated += UpdateConnectedViewers;
        }

        private void OnDisable()
        {
            BrodskyEvents.PlayersUpdated -= UpdateConnectedViewers;
        }

        private void OnRoleChosen(bool isMaster)
        {
            _masterPanel.gameObject.SetActive(isMaster);
            _clientPanel.gameObject.SetActive(!isMaster);

            BrodskyEvents.ShowStarted += Exit;
        }
        
        private void UpdateConnectedViewers(int viewers)
        {
            _viewersConnected.text = $"{viewers - 1} {BrodskyUIStrings.ViewersConnected}";
        }
    }
}
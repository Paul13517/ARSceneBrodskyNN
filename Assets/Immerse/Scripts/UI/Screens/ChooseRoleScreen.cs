using Immerse.Core.AR;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class ChooseRoleScreen : BrodskyScreen
    {
        [SerializeField] private Toggle _masterToggle;
        [SerializeField] private Button _manualConnectButton;
        
        [Space, SerializeField] private Button _arConnectButton;
        [SerializeField] private GameObject _arSelectionPanel;
        [SerializeField] private GameObject _arPanel;
        
        
        public override void Init()
        {
            _arPanel.SetActive(false);
            ARPerformanceConnector.Instance.SetEnabled(false);
            
            BrodskyEvents.RoleChosen += isMaster =>
            {
                CameraController.Instance.EnableAR(false);
                BrodskyScreenController.Instance.OpenScreen("WaitingScreen");
            };
            
            _manualConnectButton.onClick.AddListener(() =>
            {
                BrodskySettings.RoomName = "a";
                BrodskySettings.IsMaster = _masterToggle.isOn;
                BrodskyEvents.OnMultiplayerSettingsSetup();
            });

#if !UNITY_EDITOR

         _arConnectButton.onClick.AddListener(() =>
            {
                ARPerformanceConnector.Instance.SetEnabled(true);
                _arSelectionPanel.SetActive(false);
                _arPanel.SetActive(true);
                CameraController.Instance.EnableAR(true);
            });     
#endif
#if UNITY_EDITOR
            _arConnectButton.onClick.AddListener(() =>
            {
                BrodskySettings.RoomName = "a";
                BrodskySettings.IsMaster = _masterToggle.isOn;
                BrodskyEvents.OnMultiplayerSettingsSetup();
            });     
#endif

        }
    }
}
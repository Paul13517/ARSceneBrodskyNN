using Immerse.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class OutroScreen : BrodskyScreen
    {
        [SerializeField] private Button _aboutButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _supportButton;
        [SerializeField] private Button _contactsButton;

        [Space, SerializeField] private SliderScreen _aboutScreen;
        [SerializeField] private SliderScreen _creditsScreen;
        [SerializeField] private SliderScreen _supportScreen;
        [SerializeField] private SliderScreen _contactsScreen;
        
        
        public override void Init()
        {
            _aboutScreen.Init(BrodskySettings.ScreenAnimationDuration, ScreenPosition.Left, ScreenPosition.Left);
            _creditsScreen.Init(BrodskySettings.ScreenAnimationDuration, ScreenPosition.Left, ScreenPosition.Left);
            _supportScreen.Init(BrodskySettings.ScreenAnimationDuration, ScreenPosition.Left, ScreenPosition.Left);
            _contactsScreen.Init(BrodskySettings.ScreenAnimationDuration, ScreenPosition.Left, ScreenPosition.Left);

            _aboutButton.onClick.AddListener(() => _aboutScreen.Open());
            _creditsButton.onClick.AddListener(() => _creditsScreen.Open());
            _supportButton.onClick.AddListener(() => _supportScreen.Open());
            _contactsButton.onClick.AddListener(() => _contactsScreen.Open());
        }
    }
}
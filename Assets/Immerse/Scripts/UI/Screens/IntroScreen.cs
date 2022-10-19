using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class IntroScreen : BrodskyScreen
    {
        [SerializeField] private Button _startButton;

        public override void Init()
        {
            _startButton.onClick.AddListener(() =>BrodskyScreenController.Instance.OpenScreen("PermissionScreen"));
            
           // _startButton.onClick.AddListener(Exit);
        }
    }
}
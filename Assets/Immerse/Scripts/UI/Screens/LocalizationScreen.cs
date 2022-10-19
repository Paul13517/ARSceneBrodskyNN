using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Immerse.Brodsky.UI
{
    public class LocalizationScreen : BrodskyScreen
    {
        [SerializeField] private Toggle _englishToggle;
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _debug;


        public override void Init()
        {
            _englishToggle.isOn = true;
            _startButton.onClick.AddListener(() => BrodskyEvents.OnLocaleSelected(BrodskyLocale.English));
            _startButton.onClick.AddListener(() => BrodskyScreenController.Instance.OpenScreen("ChooseRoleScreen"));
        }
        
        
    }
}
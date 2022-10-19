using Immerse.Core.AR;
using UnityEngine;

namespace Immerse.Brodsky.UI
{
    public class ARCheckingScreen : BrodskyScreen
    {
        [SerializeField] private GameObject _arNotAvailablePanel;
        
        public override void Init()
        {
            _arNotAvailablePanel.SetActive(false);
            ARSupportChecker.ARSupported += OnArSupported;
        }

        private void Start()
        {
            StartCoroutine(ARSupportChecker.CheckAR());
        }

        private void OnArSupported(bool isSupported)
        {
            if (isSupported)
            {
                Exit();
            }
            else
            {
                _arNotAvailablePanel.SetActive(true);
            }
        }
    }
}
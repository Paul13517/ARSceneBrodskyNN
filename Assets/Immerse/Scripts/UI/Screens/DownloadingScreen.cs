using Immerse.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class DownloadingScreen : BrodskyScreen
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private TMP_Text _text;
        

        public override void Init()
        {
            _startButton.gameObject.SetActive(false);
            
            BrodskyEvents.DownloadProgressUpdated += _progressBar.UpdateProgress;
            BrodskyEvents.ContentDownloaded += Exit;
        }
    }
}
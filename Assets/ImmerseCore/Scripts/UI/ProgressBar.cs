using UnityEngine;

namespace Immerse.Core.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject _progressBar;
        
        public void UpdateProgress(float progress)
        {
            LeanTween.scaleX(_progressBar, progress, .2f);
        }
    }
}
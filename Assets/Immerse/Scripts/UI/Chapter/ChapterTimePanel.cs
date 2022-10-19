using System.Collections;
using TMPro;
using UnityEngine;

namespace Immerse.Brodsky.UI
{
    public class ChapterTimePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private Coroutine _countdownCoroutine;
        
        public float TimeLeft { get; private set; }


        public void Setup(float duration, bool isAr)
        {
            TimeLeft = duration;
            
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine);
            }
            
            _countdownCoroutine = StartCoroutine(RunCountdown(isAr));
        }
        
        private IEnumerator RunCountdown(bool isAr)
        {
            var waitForSecond =  new WaitForSeconds(1);
            while (TimeLeft > 0)
            {
                TimeLeft--;
                string text = isAr ? BrodskyUIStrings.ARChapterTimeLeft : BrodskyUIStrings.ChapterTimeLeft;
                _text.text = $"{TimeLeft.ToHumanTime()}\n{text}";
                yield return waitForSecond;
            }
            
            _text.text = $"{BrodskyUIStrings.ChapterTimeOver}";
        }
    }
}
using System;
using Immerse.Brodsky.Data;
using Immerse.Brodsky.PUN;
using Immerse.Core.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Immerse.Brodsky.UI
{
    public class ChapterButtonsPanel : MonoBehaviour
    {
        [SerializeField] private SliderButton _runNext;
        [SerializeField] private ImmerseButton _runAR;
        [SerializeField] private SliderButton _finishAR;
        [SerializeField] private SliderButton _finishShow;

        private Color _enabledColor;
        private Func<bool> _popUpCondition;
        

        public void Init(UnityAction runArCallback, Func<bool> popUpCondition)
        {
            _runAR.Init(runArCallback);
            _runAR.SetText(BrodskySettings.IsMaster ? BrodskyUIStrings.ARButtonMasterText : BrodskyUIStrings.ARButtonClientText);

            _popUpCondition = popUpCondition;
            _runNext.Init(() => SliderButtonCallback(_runNext.Reset));
            _finishAR.Init(() => SliderButtonCallback(_finishAR.Reset));
            _finishShow.Init(() => SliderButtonCallback(_finishShow.Reset));
            
            SetArButtonsEnabled(false);
        }

        public void Setup(bool hasAR, bool isLastChapter)
        {
            _runNext.Reset();
            _finishAR.Reset();
            _finishShow.Reset();
            _runNext.gameObject.SetActive(BrodskySettings.IsMaster && !hasAR && !isLastChapter);
            _finishShow.gameObject.SetActive(BrodskySettings.IsMaster && !hasAR && isLastChapter);

            _runAR.gameObject.SetActive(hasAR);
            _finishAR.gameObject.SetActive(hasAR && BrodskySettings.IsMaster);

            if (hasAR)
            {
                SetArButtonsEnabled(false);
            }
        }

        public void SetArButtonsEnabled(bool isEnabled)
        {
            _finishAR.SetEnabled(isEnabled);
            _runAR.SetEnabled(isEnabled);
        }

        private void SliderButtonCallback(Action cancelCallback)
        {
            BrodskyPopUps.ShowChapterNotOver(()=>BrodskyShow.OnChapterFinished(BrodskyShow.CurrentIndex+1), cancelCallback, _popUpCondition);
        }
    }
}
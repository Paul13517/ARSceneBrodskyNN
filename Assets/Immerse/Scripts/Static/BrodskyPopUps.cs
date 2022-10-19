using System;
using System.Collections.Generic;
using Immerse.Brodsky.UI;
using Immerse.Core.UI;

namespace Immerse.Brodsky
{
    public static class BrodskyPopUps
    {
        public static void ShowChapterNotOver(Action actionCallback, Action cancelCallback,  Func<bool> popUpCondition)
        {
            if (!popUpCondition())
            {
                actionCallback?.Invoke();
                return;
            }

            PopUpOptions chapterNotOverPopUp = new PopUpOptions
            (
                null, 
                new List<PopUpButtonOptions>
                {
                    new PopUpButtonOptions(BrodskyUIStrings.FinishChapter, true, actionCallback),
                    new PopUpButtonOptions(BrodskyUIStrings.RunAnyway, cancelCallback)
                },
                BrodskyUIStrings.ChapterNotOver
            );
            
            PopUp.Instance.Show(chapterNotOverPopUp);
        }

        public static void ShowChapterChangePopUp(Chapter chapter, Action actionCallback)
        {
            PopUpOptions chapterChangePopUp = new PopUpOptions
            (
                null,
                new List<PopUpButtonOptions>
                {
                    new PopUpButtonOptions(BrodskyUIStrings.Cancel, true),
                    new PopUpButtonOptions(BrodskyUIStrings.Yes, actionCallback)
                },
                BrodskyUIStrings.AreYouSure
            );
            
            PopUp.Instance.Show(chapterChangePopUp);
        }
    }
}
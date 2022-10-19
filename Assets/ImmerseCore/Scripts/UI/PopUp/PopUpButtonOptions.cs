using System;

namespace Immerse.Core.UI
{
    public class PopUpButtonOptions
    {
        public string text;
        public bool preferedChoise;
        public Action callback;


        public PopUpButtonOptions(string text) : this(text, false, null)
        {
        }
        
        public PopUpButtonOptions(string text, Action callback) : this(text, false, callback)
        {
        }

        public PopUpButtonOptions(string text, bool preferedChoise) : this(text, preferedChoise, null)
        {
        }
        
        public PopUpButtonOptions(string text, bool preferedChoise, Action callback)
        {
            this.text = text;
            this.preferedChoise = preferedChoise;
            this.callback = callback;
        }
    }
}
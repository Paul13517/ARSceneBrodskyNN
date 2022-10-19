using System.Collections.Generic;

namespace Immerse.Core.UI
{
    public class PopUpOptions
    {
        public string header;
        public List<PopUpButtonOptions> buttonsOptions;
        public string message;


        public PopUpOptions(string message, List<PopUpButtonOptions> buttonsOptions, string header = null)
        {
            this.header = header;
            this.buttonsOptions = buttonsOptions;
            this.message = message;
        }
    }
}
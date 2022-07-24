using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Web.View.Components.Universal.Dropdowns
{
    public abstract class DropdownMenuOption
    {
        public string Text;
        public class Link : DropdownMenuOption
        {
            public string Href;
        }
        public class Button : DropdownMenuOption
        {
            public Action OnClick;
        }
    }
}

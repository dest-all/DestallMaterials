using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public class ComponentControlContainer<TControlTool>
        where TControlTool : class
    {
        TControlTool _tool;
        public TControlTool Tool { get => _tool; set { _tool = _tool ?? value; } }
    }
}

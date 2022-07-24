using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Components
{
    public abstract class StandardItemComponent<TModel>
    {
        public abstract TModel Model { get; set; }
        public abstract RenderFragment Render();
    }

    public abstract class ViewTableStandardComponent<TModel, TFilter> : StandardItemComponent<IList<TModel>>
    { 
        public abstract TFilter Filter { get; set; }
    }

}

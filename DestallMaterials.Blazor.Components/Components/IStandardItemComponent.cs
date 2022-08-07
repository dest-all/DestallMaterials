using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Components
{
    public interface IStandardItemComponent<TModel>
    {
        TModel Model { get; set; }
        RenderFragment Render();
        Action<TModel> OnChange { get; set; }
    }

    public interface IStandardItemForm<TModel> : IStandardItemComponent<TModel>
    {
    }

    public interface IViewTableStandardComponent<TModel, TFilter> : IStandardItemComponent<IList<TModel>>
    {
    }

    public interface ITableStandardComponent<TModel, TFilter> : IViewTableStandardComponent<TModel, TFilter>
    {
    }
}

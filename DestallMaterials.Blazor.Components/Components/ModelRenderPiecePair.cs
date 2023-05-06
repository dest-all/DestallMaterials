using DestallMaterials.Blazor.Components.Universal.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Components;

sealed class ModelRenderPiecePair<TModel>
{
    public ModelRenderPiecePair(TModel model)
    {
        this.Model = model;
    }

    public TModel Model { get; set; }
    public RenderPiece RenderPiece { get; set; }
}

using DestallMaterials.WheelProtection.DataStructures;
using Microsoft.AspNetCore.Components;

namespace DestallMaterials.Blazor.Components.Universal;

public class DisposableComponent : ComponentBase, IDisposable
{
    DisposableList<IDisposable> BoundItems = new DisposableList<IDisposable>();

    public void Dispose()
    {
        BeforeDispose();
        BoundItems.Dispose();
    }

    protected virtual void BeforeDispose()
    {
        
    }


    protected void BindToLifetime(IDisposable disposable) => BoundItems.Add(disposable);
}

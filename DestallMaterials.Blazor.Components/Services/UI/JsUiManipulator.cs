using Microsoft.JSInterop;

namespace DestallMaterials.Blazor.Services.UI
{
    public class JsUiManipulator : IUiManipulator
    {
        private readonly IJSRuntime _runtime;

        public JsUiManipulator(IJSRuntime runtime)
        {
            this._runtime = runtime;
        }

        public async Task SayHi(string name)
        {
            await _runtime.InvokeVoidAsync("sayHi", name);
        }

        public void ScrollItem_XAxis(string id, double deltaX)
        {
            throw new NotImplementedException();
        }

        public void ScrollItem_YAxis(string id, double deltaY)
        {
            throw new NotImplementedException();
        }
    }
}

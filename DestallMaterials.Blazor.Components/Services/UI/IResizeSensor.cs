﻿using DestallMaterials.WheelProtection.Extensions.Enumerables;
using Microsoft.JSInterop;

namespace DestallMaterials.Blazor.Services.UI
{
    public struct ElementSize
    {
        public double Height { get; init; }
        public double Width { get; init; }
    }

    public interface IResizeSensor : IElementSensor<ElementSize>
    {
    }

    public class JsResizeSensor : IResizeSensor
    {
        readonly Dictionary<string, List<Func<ElementSize, Task>>> _subscriptions = new();
        readonly IJSRuntime _js;

        const string _subscribeCommand = "destallMaterials.sensors.resize.subscribe";

        public JsResizeSensor(IJSRuntime js)
        {
            _js = js;
        }

        [JSInvokable]
        public async Task ReactAsync(
            string elementId,
            double[] elementScrollStateNumbers)
        {
            if (_subscriptions.TryGetValue(elementId, out var callbacks))
            {
                ElementSize elementState = elementScrollStateNumbers.HasContent() ? new ElementSize
                {
                    Height = elementScrollStateNumbers[0],
                    Width = elementScrollStateNumbers[1]
                } : new();

                foreach (var callback in callbacks)
                {
                    await callback(elementState);
                }
                return;
            }
        }

        public async Task<DisposableCallback> SubscribeAsync(string id, Func<ElementSize, Task> callback)
        {
            if (_subscriptions.TryGetValue(id, out var callbacks))
            {
                callbacks.Add(callback);
            }
            else
            {
                _subscriptions[id] = new()
                {
                    callback
                };

                await _js.InvokeVoidAsync(_subscribeCommand, id, DotNetObjectReference.Create(this));
            }

            var result = new DisposableCallback(() => _subscriptions[id].Remove(callback));

            return result;
        }
    }

}

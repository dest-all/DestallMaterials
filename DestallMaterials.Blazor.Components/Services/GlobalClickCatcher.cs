using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Services
{
    public class GlobalClickCatcher : IGlobalClickCatcher, IGlobalClickInvoker
    {
        readonly List<Action<KeyboardEventArgs>> _keyClickCallbacks = new();
        readonly List<Action<MouseEventArgs>> _mouseClickCallbacks = new();

        public void FireGlobalMouseClickEvent(MouseEventArgs eventArgs)
        {
            foreach (var callback in _mouseClickCallbacks)
            {
                callback(eventArgs);
            }
        }

        public void FireKeyClickEvent(KeyboardEventArgs eventArgs)
        {
            foreach (var callback in _keyClickCallbacks)
            {
                callback(eventArgs);
            }
        }

        public DisposableCallback SubscribeForGlobalClick(Action<MouseEventArgs> onMouseClick)
        {
            var dc = new DisposableCallback(e => _mouseClickCallbacks.Remove(onMouseClick));
            _mouseClickCallbacks.Add(onMouseClick);
            return dc;
        }

        public DisposableCallback SubscribeForKeyClick(Action<KeyboardEventArgs> onKeyClick)
        {
            var dc = new DisposableCallback(e => _keyClickCallbacks.Remove(onKeyClick));
            _keyClickCallbacks.Add(onKeyClick);
            return dc;
        }
    }
}

using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public class DisposableCallback : IDisposable
    {
        readonly Action<DisposableCallback> _onDisposed;

        public DisposableCallback(Action<DisposableCallback> onDisposed)
        {
            _onDisposed = onDisposed;
        }

        public void Dispose()
        {
            _onDisposed(this);
        }
    }
    public interface IGlobalClickCatcher
    {
        DisposableCallback SubscribeForGlobalClick(Action<MouseEventArgs> onMouseClick);
        DisposableCallback SubscribeForKeyClick(Action<KeyboardEventArgs> onKeyClick);
    }

    public interface IGlobalClickInvoker
    {
        void FireGlobalMouseClickEvent(MouseEventArgs eventArgs);
        void FireKeyClickEvent(KeyboardEventArgs eventArgs);
    }
}
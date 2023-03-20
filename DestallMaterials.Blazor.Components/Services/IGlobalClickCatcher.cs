﻿using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Services
{
    public class DisposableCallback : IDisposable
    {
        readonly Action<DisposableCallback> _onDisposed;

        public DisposableCallback(Action<DisposableCallback> onDisposed)
        {
            _onDisposed = onDisposed;
        }

        public DisposableCallback(Action onDisposed)
        {
            _onDisposed = th => onDisposed();
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


        DisposableCallback SubscribeForGlobalClick(Func<MouseEventArgs, Task> onMouseClick);
        DisposableCallback SubscribeForKeyClick(Func<KeyboardEventArgs, Task> onKeyClick);
    }

    public interface IGlobalClickInvoker
    {
        Task FireGlobalMouseClickEvent(MouseEventArgs eventArgs);
        Task FireKeyClickEvent(KeyboardEventArgs eventArgs);
    }
}
using Client.Web.View.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Components
{
    public abstract class ViewComponent : ComponentBase, IDisposable
    {
        protected DisposableCollection Callbacks = new();

        protected DisposableCallback Subscribe(DisposableCallback callback)
        {
            Callbacks.Add(callback);
            return callback;
        }

        protected DisposableCallback[] Subscribe(DisposableCallback callback1, DisposableCallback callback2, params DisposableCallback[] other)
        {
            var result = new DisposableCallback[other.Length + 2];
            for (int i = 2; i < result.Length; i++)
            {
                result[i] = other[i - 2];
            }
            result[0] = callback1;
            result[1] = callback2;
            Callbacks.Add(result);
            return result;
        }

        public virtual void Dispose()
        {
            Callbacks.Dispose();
        }
    }
}

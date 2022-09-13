using DestallMaterials.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Components
{
    public abstract partial class ClickableComponent : ViewComponent
    {
        [Inject]
        protected IGlobalClickCatcher globalClickCatcher { get; private set; }

        [Parameter]
        public bool Disabled { get; set; }

        protected virtual async Task OnInnerClickAsync(MouseEventArgs mouseEventArgs)
        {
        }

        protected virtual async Task OnOuterClickAsync(MouseEventArgs mouseEventArgs)
        { 
        }

        protected void OnGlobalClick(MouseEventArgs mouseEventArgs)
        {
            if (_mouseIn)
            {
                OnInnerClickAsync(mouseEventArgs);
            }
            else
            {
                OnOuterClickAsync(mouseEventArgs);
            }
        }

        protected bool _mouseIn;

        protected ClickableComponent()
        {
            _onMouseIn = () => _mouseIn = true;
            _onMouseOut = () => _mouseIn = false;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                Subscribe(globalClickCatcher.SubscribeForGlobalClick(OnGlobalClick));
            }
        }

        protected readonly Action _onMouseIn;
        protected readonly Action _onMouseOut;
    }
}

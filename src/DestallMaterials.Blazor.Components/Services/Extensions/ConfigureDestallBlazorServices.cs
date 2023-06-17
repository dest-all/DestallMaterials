using DestallMaterials.Blazor.Services.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Services.Extensions
{
    public static class ServicesInjectExtensions
    {
        public static IServiceCollection ConfigureDestallBlazorServices(this IServiceCollection services) 
            => services
                .AddSingleton<IUiManipulator, JsUiManipulator>()
                .AddSingleton<IScrollSensor, JsScrollSensor>()
                .AddSingleton<IResizeSensor, JsResizeSensor>();
    }
}

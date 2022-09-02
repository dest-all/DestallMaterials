using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Reflection;

namespace DestallMaterials.Blazor.Services.UI.Extensions
{
    public static class UiFunctions
    {
        
    }

    public class VirtualizerNumbers
    {
        public readonly int ItemsBefore;
        public readonly int VisibleItemCapacity;
        public readonly int ItemsCount;

        public VirtualizerNumbers(int itemsBefore, int visibleItemCapacity, int itemsCount)
        {
            ItemsBefore = itemsBefore;
            VisibleItemCapacity = visibleItemCapacity;
            ItemsCount = itemsCount;
        }
    }
}

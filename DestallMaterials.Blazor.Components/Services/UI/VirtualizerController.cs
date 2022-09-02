using DestallMaterials.Blazor.Services.UI.Extensions;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Reflection;

namespace DestallMaterials.Blazor.Services.UI
{
    public class VirtualizerController<TVirtualize>
    {
        static readonly IReadOnlyList<FieldInfo> _virtualizerUnjustlyPrivateFields = new List<FieldInfo>
        {
            typeof(Virtualize<TVirtualize>).GetField("_itemsBefore", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException(),
            typeof(Virtualize<TVirtualize>).GetField("_itemCount", BindingFlags.NonPublic | BindingFlags.Instance ) ?? throw new InvalidOperationException(),
            typeof(Virtualize<TVirtualize>).GetField("_visibleItemCapacity", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException()
        };
        public static VirtualizerNumbers GetVirtualizerNumbers<T>(Virtualize<T> virtualize) => new VirtualizerNumbers(
                (int)_virtualizerUnjustlyPrivateFields[0].GetValue(virtualize),
                (int)_virtualizerUnjustlyPrivateFields[1].GetValue(virtualize),
                (int)_virtualizerUnjustlyPrivateFields[2].GetValue(virtualize)
            );

        readonly IUiManipulator _uiManipulator;
        readonly Virtualize<TVirtualize> _virtualize;
        readonly string _containerId;
        readonly Func<int> _itemsCount;
        public VirtualizerController(IUiManipulator uiManipulator, Virtualize<TVirtualize> virtualize, string containerId, Func<int> itemsTotalCount)
        {
            _uiManipulator = uiManipulator;
            _virtualize = virtualize;
            _containerId = containerId;
            _itemsCount = itemsTotalCount;
        }

        public async Task ScrollToItem(int itemIndex)
        {
            var scrollTo = itemIndex * _virtualize.ItemSize;
            await _uiManipulator.ScrollItem_Y(_containerId, scrollTo);
        }
        public VirtualizerNumbers Numbers => GetVirtualizerNumbers(_virtualize);
    }
}

﻿using DestallMaterials.Blazor.Functions;
using DestallMaterials.Blazor.Services.Extensions;
using DestallMaterials.Blazor.Services.UI;
using DestallMaterials.Blazor.Services;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestallMaterials.WheelProtection.Extensions.String;

namespace DestallMaterials.Blazor.Components.Universal.Inputs
{
    public partial class SelectionList<TItem> : ClickableComponent
    {
        [Parameter]
        public uint MaxItemsShown { get; set; } = 10;

        [Parameter]
        public Action<TItem> OnItemClicked { get; set; } = i => { };

        [Parameter]
        public Action<TItem> OnItemExcluded { get; set; } = i => { };

        [Parameter]
        [EditorRequired]
        public int ItemsCountTotal { get; set; }

        [Parameter]
        [EditorRequired]
        public Func<uint, Task<IList<TItem>>> GetBatch { get; set; }

        [Parameter]
        public uint BatchSize { get; set; } = 10;

        [Parameter]
        public Func<TItem, string> GetItemRepresentation { get; set; } = i => i.ToString();

        [Parameter]
        public Func<TItem, TItem, bool> ItemsComparison { get; set; } = (i1, i2) => i1.Equals(i2);

        [Parameter]
        public string InputLabel { get; set; }

        [Parameter]
        public Func<TItem, bool> ItemIsEmpty { get; set; } = i => i == null;

        [Parameter]
        public Action OnListClick { get; set; } = null;

        [Parameter]
        public bool IsActive { get; set; }

        [Parameter]
        public Action<TItem> OnArrowSelect { get; set; } = item => { };

        [Parameter]
        public Action OnFocus { get; set; } = () => { };

        private void _onFocus()
        {
            if (OnFocus != null)
            {
                OnFocus();
            }
        }

        Virtualize<TItem> Virtualize;

        readonly string _elementId = Guid.NewGuid().ToString();

        async Task<IEnumerable<TItem>> DownloadItemsOnDemandAsync(uint startIndex, uint count, uint pageSize)
        {
            return await DynamicLoading.LoadForVirtualizationInPagesAsync(
                (int)startIndex,
                (int)count,
                pageSize,
                async p => await GetBatch(p)
            );
        }

        TItem SelectedItem;

        async Task<ItemsProviderResult<TItem>> ProvideItems(ItemsProviderRequest request)
        {
            var items = await DownloadItemsOnDemandAsync((uint)request.StartIndex, (uint)request.Count, BatchSize);
            if (!items.Any())
            {
                SelectedItemIndex = -1;
            }
            return new(items, ItemsCountTotal);
        }

        public int SelectedItemIndex { get; private set; } = -1;

        async Task ClickItem(TItem item)
        {
            OnItemClicked(item);

            Console.WriteLine(new
            {
                CurrentSelected = SelectedItemIndex,
                Indexes = await VirtualizerController.GetTopAndBottomIndexesAsync()
            }.ToJson());
            StateHasChanged();
        }

        void SpacePressed()
        {
            if (SelectedItemIndex != -1)
            {
                var selectedItem = SelectedItem;
                if (!ItemIsEmpty(selectedItem))
                {
                    OnItemClicked(SelectedItem);
                }
            }
        }

        void OnMouseIn()
        {
            _mouseIn = true;
        }
        void OnMouserOut()
        {
            _mouseIn = false;
        }

        async Task ArrowClicked(ArrowDirection arrowDirection)
        {
            if (!IsActive || VirtualizerController == null)
            {
                return;
            }
            var topBottom = await VirtualizerController.GetTopAndBottomIndexesAsync();

            SelectedItemBoundaries = topBottom;

            if (SelectedItemIndex <= topBottom.Bottom && SelectedItemIndex >= topBottom.Top)
            {
                return;
            }

            var scrollDestination = arrowDirection switch
            {
                ArrowDirection.Up => ScrollDestination.Top,
                ArrowDirection.Down => ScrollDestination.Bottom,
                _ => throw new NotImplementedException()
            };

            await VirtualizerController.ScrollToLineAsync(SelectedItemIndex, scrollDestination);
        }

        async Task ArrowUp()
        {
            if (SelectedItemIndex > 0)
            {
                SelectedItemIndex--;
                await ArrowClicked(ArrowDirection.Up);
                StateHasChanged();
            }
        }

        async Task ArrowDown()
        {
            if (SelectedItemIndex < ItemsCountTotal - 1)
            {
                SelectedItemIndex++;
                await ArrowClicked(ArrowDirection.Down);
                StateHasChanged();
            }
        }


        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Subscribe(globalClickCatcher.OnKeyPressed(Key.ArrowUp, async e => await ArrowUp()));
                Subscribe(globalClickCatcher.OnKeyPressed(Key.ArrowDown, async e => await ArrowDown()));
                Subscribe(globalClickCatcher.OnKeyPressed(Key.Space, e => SpacePressed()));
                Subscribe(globalClickCatcher.OnKeyPressed(Key.Enter, e => SpacePressed()));
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await uiManipulator.DisableDefaultEventHandling(_elementId, "onkeydown");
            }
        }

        protected override void OnParametersSet()
        {
            if (TopBottomIndexes.Top == 0 && TopBottomIndexes.Bottom == 0)
            {
                TopBottomIndexes = new TopBottomIndexPair
                {
                    Top = 0,
                    Bottom = (int)Math.Min(MaxItemsShown, ItemsCountTotal)
                };
            }

            var listHeight = _listLineHeight * Math.Min(MaxItemsShown, ItemsCountTotal);
            ListStyle = $"height: {listHeight}px; line-height: {_listLineHeight}px";
        }

        VirtualizerController<TItem> VirtualizerController;

        public TopBottomIndexPair TopBottomIndexes { get; private set; }

        string ListStyle;

        ElementReference _listReference;

        IList<TItem> Items = new List<TItem>();

        const int _listLineHeight = 20;
    }

    public enum ArrowDirection
    {
        Up, Down
    }
}

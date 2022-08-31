using DestallMaterials.Blazor.Services;
using DestallMaterials.Blazor.Services.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Components.Universal.Inputs
{
    public abstract partial class SearchSelectorBase<TItem> : ClickableComponent
    {

        [Parameter]
        public TItem SelectedItem { get; set; }

        [Parameter]
        public Func<uint, string, Task<IList<TItem>>> GetBatch { get; set; }

        [Parameter]
        public uint BatchSize { get; set; }

        [Parameter]
        public Func<TItem, string> GetItemRepresentation { get; set; }

        [Parameter]
        public Action<TItem> OnItemSelect { get; set; } = o => { };

        [Parameter]
        public Func<TItem, TItem, bool> ItemsComparison { get; set; }

        [Parameter]
        public string InputLabel { get; set; }

        [Parameter]
        public Func<TItem, bool> ItemEmpty { get; set; } = i => i == null;

        protected bool AnythingSelected;

        protected virtual IList<TItem> ProcessBatch(IList<TItem> incomingBatch) => incomingBatch;

        protected uint _currentBatchNumber = 1;

        protected IList<TItem> _currentBatch = new List<TItem>();
        protected bool _showList { get; set; } = false;

        protected string _filterString { get; set; }

        protected async Task OnFilterChange(string filterValue)
        {
            _filterString = filterValue;
            _currentBatch = ProcessBatch(await GetBatch(_currentBatchNumber, filterValue));
            if (_currentBatch.Any())
            {
                ToggleList(true);
            }
            base.StateHasChanged();
        }

        protected virtual void ToggleList(bool onoff)
        {
            if (_showList == onoff) { return; }
            _showList = onoff;

            if (_showList == true)
            {
                SelectedItem = _currentBatch.FirstOrDefault();
            }
            else
            {
                _currentBatch.Clear();
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                ConfigureClickHandlers();
            }
        }

        protected abstract void OnEnterClicked(TItem currentItem);

        protected virtual void ConfigureClickHandlers()
        {
            Subscribe(
               globalClickCatcher.OnKeyPressed(Key.ArrowDown, e =>
               {
                   if (_currentBatch.Any())
                   {
                       int selectedIndex = SelectedItem != null ? _currentBatch.IndexOf(SelectedItem) : (-1);
                       if (selectedIndex < _currentBatch.Count - 1)
                       {
                           _onItemSelect(_currentBatch[selectedIndex + 1]);
                       }
                       else
                       {
                           _onItemSelect(_currentBatch.First());
                       }
                   }
               }),
               globalClickCatcher.OnKeyPressed(Key.ArrowUp, e =>
               {
                   if (_currentBatch.Any())
                   {
                       int selectedIndex = SelectedItem != null ? _currentBatch.IndexOf(SelectedItem) : (_currentBatch.Count - 1);
                       if (selectedIndex > 0)
                       {
                           _onItemSelect(_currentBatch[selectedIndex - 1]);
                       }
                       else
                       {
                           _onItemSelect(_currentBatch[_currentBatch.Count - 1]);
                       }
                   }
               }),
               globalClickCatcher.OnKeyPressed(Key.Enter, e =>
               {
                   ToggleList(false);
                   if (SelectedItem != null)
                   {
                       OnEnterClicked(SelectedItem);
                   }
                   _filterString = "";
                   StateHasChanged();
               })
           );
        }

        protected virtual void _onItemSelect(TItem item)
        {
        }
    }
}

namespace DestallMaterials.Blazor.Services;

public interface IItemsListControlTool<TItem>
{
    Task<TItem> AddAsync();
    Task RefreshAsync();
    IReadOnlyCollection<uint> GetSelected();
}

public class ItemsListControlTool<TItem> : IItemsListControlTool<TItem>
{
    readonly Func<Task<TItem>> _add;
    readonly Func<Task> _refresh;
    readonly Func<IReadOnlyCollection<uint>> _getSelected;
    public ItemsListControlTool(
            Func<Task<TItem>> add,
            Func<Task> refresh,
            Func<IReadOnlyCollection<uint>> getSelected
        )
    {
        _add = add;
        _refresh = refresh;
        _getSelected = getSelected;
    }

    public IReadOnlyCollection<uint> GetSelected() => _getSelected();
    public Task<TItem> AddAsync() => _add();
    public Task RefreshAsync() => _refresh();
}

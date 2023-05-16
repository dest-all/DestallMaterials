using DestallMaterials.Blazor.Components.Universal;
using MudBlazor;

namespace Client.Web.View.Components.Configurations;

public class ButtonConfiguration
{
    public ButtonConfiguration()
    {
    }

    public bool Disabled { get; set; } = true;

    public Func<Task> Callback { get; init; } = () => Task.CompletedTask;

    public string ActionName { get; init; } = "";

    public string Icon { get; init; } = "";

    public Color Color { get; init; } = Color.Info; 

    public bool Hidden { get; set; }

    public TimeSpan ShowStateFor { get; set; } = TimeSpan.FromSeconds(1);

    public Action AfterSuccess { get; set; } = () => { };
}

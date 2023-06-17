using DestallMaterials.Blazor.Services.Extensions;
using DestallMaterials.Blazor.Services.UI;
using DestallMaterials.Blazor.VisualTesting;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.ConfigureDestallBlazorServices();

builder.Services.AddMudServices();

await builder.Build().RunAsync();

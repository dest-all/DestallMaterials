using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApplication;
using MudBlazor.Services;
using DestallMaterials.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var clickCatcher = new GlobalClickCatcher();

builder.Services.AddSingleton<IGlobalClickCatcher>(clickCatcher);
builder.Services.AddSingleton<IGlobalClickInvoker>(clickCatcher);
builder.Services.AddMudServices();

await builder.Build().RunAsync();

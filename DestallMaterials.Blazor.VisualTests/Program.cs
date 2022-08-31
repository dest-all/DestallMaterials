using DestallMaterials.Blazor.Services;
using DestallMaterials.Blazor.VisualTests;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var clickCatcher = new GlobalClickCatcher();

builder.Services.AddSingleton<IGlobalClickCatcher>(clickCatcher);
builder.Services.AddSingleton<IGlobalClickInvoker>(clickCatcher);

await builder.Build().RunAsync();

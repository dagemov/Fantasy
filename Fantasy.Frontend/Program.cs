using Fantasy.Frontend;
using Fantasy.Frontend.Extensions;
using Fantasy.Frontend.Repositories.Interfaces;
using Fantasy.Frontend.Repositories.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddServiceApp(builder.Configuration);

await builder.Build().RunAsync();
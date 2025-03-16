using Keros;
using Keros.Authentication.Azure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

// Register application login providers
builder.Services.AddAzureLogin(builder.Configuration, builder.HostEnvironment);

builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();

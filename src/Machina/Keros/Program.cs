using Keros;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

// Authentication configuration for the application service
// Ref: https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-single-page-app-sign-in
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
});

// Replacing an HttpClient declaration with the one with authorization handler for authenticated calls.
builder.Services.AddScoped(serviceProvider =>
{
    var authorizationMessageHandler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();
    authorizationMessageHandler.InnerHandler = new HttpClientHandler();
    authorizationMessageHandler.ConfigureHandler(authorizedUrls: ["https://graph.microsoft.com/v1.0"], scopes: ["User.Read"]);

    return new HttpClient(authorizationMessageHandler) { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
});

builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();

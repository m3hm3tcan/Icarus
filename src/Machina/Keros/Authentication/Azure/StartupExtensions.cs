using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Keros.Authentication.Azure;

/// <summary>
/// Contains application startup extensions for Azure authentication services.
/// </summary>
public static class StartupExtensions
{
    /// <summary>
    /// Microsoft Graph API URL.
    /// </summary>
    public const string GraphApiUrl = "https://graph.microsoft.com";

    /// <summary>
    /// Microsoft Graph API default scope.
    /// </summary>
    public const string GraphScopeDefault = "User.Read";

    /// <summary>
    /// Configures Azure login authentication for an application service using Microsoft Authentication Library.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> container.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
    /// <param name="environment">The <see cref="IWebAssemblyHostEnvironment"/> instance.</param>
    public static void AddAzureLogin(this IServiceCollection services, IConfiguration configuration, IWebAssemblyHostEnvironment environment)
    {
        // Application configured according to Microsoft's documentation.
        // Ref: https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-single-page-app-sign-in
        services.AddMsalAuthentication(options =>
        {
            configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            options.ProviderOptions.DefaultAccessTokenScopes.Add($"{GraphApiUrl}/{GraphScopeDefault}");
        });

        // Add authentication handler for protected urls.
        services.AddAuthenticationHandler(environment);
        services.AddProtectionMap(urls: [$"{GraphApiUrl}/v1.0"], scopes: [GraphScopeDefault]);

        // Add login provider.
        services.AddSingleton<ILoginProvider, AzureLoginProvider>();
    }
}
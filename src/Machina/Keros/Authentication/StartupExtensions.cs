using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Keros.Authentication;

/// <summary>
/// Contains application startup extensions for login providers.
/// </summary>
public static class StartupExtensions
{
    /// <summary>
    /// Configures authentication handler for the <see cref="HttpClient"/> calls to protected urls.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> container.</param>
    /// <param name="environment">The <see cref="IWebAssemblyHostEnvironment"/> instance.</param>
    public static void AddAuthenticationHandler(this IServiceCollection services, IWebAssemblyHostEnvironment environment)
    {
        services.AddScoped(serviceProvider =>
        {
            var handler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();

            handler.InnerHandler = new HttpClientHandler();
            foreach (var mapping in serviceProvider.GetServices<AuthorizationMapping>())
            {
                handler.ConfigureHandler(authorizedUrls: mapping.Urls, scopes: mapping.Scopes);
            }

            return new HttpClient(handler) { BaseAddress = new Uri(environment.BaseAddress) };
        });
    }

    /// <summary>
    /// Configures an authorization mapping with specified URLs and scopes in the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> container.</param>
    /// <param name="urls">The sequence of urls to protect with authorization.</param>
    /// <param name="scopes">The sequence of scopes to request authorization for.</param>
    public static void AddProtectionMap(this IServiceCollection services, IEnumerable<string> urls, IEnumerable<string> scopes)
    {
        services.AddSingleton(new AuthorizationMapping(urls, scopes));
    }
}
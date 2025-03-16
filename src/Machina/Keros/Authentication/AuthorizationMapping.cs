namespace Keros.Authentication;

/// <summary>
/// Represents a mapping for authorized URLs to specific scopes for authorization purposes.
/// </summary>
/// <param name="urls">The sequence of urls to protect with authorization.</param>
/// <param name="scopes">The sequence of scopes to request authorization for.</param>
/// <exception cref="ArgumentNullException">When required argument is null.</exception>
public class AuthorizationMapping(IEnumerable<string> urls, IEnumerable<string> scopes)
{
    /// <summary>
    /// The sequence of urls to protect with authorization.
    /// </summary>
    public IEnumerable<string> Urls { get; } = urls ?? throw new ArgumentNullException(nameof(urls));

    /// <summary>
    /// The sequence of scopes to request authorization for.
    /// </summary>
    public IEnumerable<string> Scopes { get; } = scopes ?? throw new ArgumentNullException(nameof(scopes));
}
namespace Keros.Authentication;

/// <summary>
/// Represents a login provider service.
/// </summary>
public interface ILoginProvider
{
    /// <summary>
    /// The name of an identity provider service.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The optional identity provider icon.
    /// </summary>
    public string? Icon { get; }
}
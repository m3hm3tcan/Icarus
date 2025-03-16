namespace Keros.Authentication.Azure;

/// <summary>
/// Represents the Azure login provider service.
/// </summary>
public class AzureLoginProvider : ILoginProvider
{
    /// <inheritdoc/>
    public string Name => "Microsoft Azure";

    /// <inheritdoc/>
    public string? Icon => null;
}

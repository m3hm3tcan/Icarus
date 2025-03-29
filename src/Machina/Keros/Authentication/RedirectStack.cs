using System.Diagnostics.CodeAnalysis;

namespace Keros.Authentication;

/// <summary>
/// Represents the redirection operational stack that handles source context states between page transitions.
/// This is used to manage the redirection flow in the application.
/// </summary>
public class RedirectStack
{
    private readonly Stack<string> _operations = new();

    /// <summary>
    /// Pushed the requested return URL to the operational stack.
    /// </summary>
    /// <param name="returnUrl">The return URL that is requested to be used when navigating back.</param>
    public void Request(string returnUrl)
    {
        if (_operations.Count > 0)
        {
            _operations.Clear();
        }
        _operations.Push(returnUrl);
    }

    /// <summary>
    /// Attempts to restore the last requested return URL from the operational stack.
    /// </summary>
    /// <param name="returnUrl">The return URL if operations is successful; otherwise, null.</param>
    /// <returns>True if the operation is successful; otherwise, fasle.</returns>
    public bool TryRestore([MaybeNullWhen(false)] out string returnUrl)
    {
        return _operations.TryPop(out returnUrl);
    }
}
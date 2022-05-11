using JetBrains.Annotations;

namespace GW2SDK;

/// <summary>A hyperlink is usually obtained as part of the result of another resource request. It can contain the URI of
/// related resources or the URI of the original resource itself.</summary>
/// <remarks>Not meant to be used directly.</remarks>
[PublicAPI]
public readonly record struct Hyperlink(string Url)
{
    public static readonly Hyperlink None = new("");

    public bool IsEmpty => string.IsNullOrEmpty(Url);
}

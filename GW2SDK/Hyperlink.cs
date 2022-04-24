using JetBrains.Annotations;

namespace GW2SDK;

/// <summary>A hyperlink is usually obtained as part of the result of another resource request. It can contain the URI of
/// related resources or the URI of the original resource itself.</summary>
/// <remarks>Not meant to be used directly.</remarks>
[PublicAPI]
public sealed record Hyperlink(string Url)
{
    public static Hyperlink None = new("");

    public bool IsEmpty => ReferenceEquals(this, None) || string.IsNullOrEmpty(Url);
}

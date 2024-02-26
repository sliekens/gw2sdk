namespace GuildWars2;

/// <summary>Represents a hyperlink.</summary>
/// <remarks>Not meant to be used directly. A hyperlink is usually obtained from an API response. For example, to link to
/// related resources.</remarks>
/// <param name="Url">The URL of the hyperlink.</param>
[PublicAPI]
public readonly record struct Hyperlink(string Url)
{
    /// <summary>Represents an empty hyperlink.</summary>
    public static readonly Hyperlink None = new("");

    /// <summary>Gets a value indicating whether the hyperlink is empty.</summary>
    public bool IsEmpty => string.IsNullOrEmpty(Url);
}

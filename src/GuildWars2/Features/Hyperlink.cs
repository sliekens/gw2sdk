namespace GuildWars2;

/// <summary>Represents a hyperlink.</summary>
/// <remarks>Not meant to be used directly. A hyperlink is usually obtained from an API response. For example, to link to
/// related resources.</remarks>
public readonly record struct Hyperlink
{
    /// <summary>Initializes a new instance of the <see cref="Hyperlink"/> struct.</summary>
    /// <param name="uri">The URL of the hyperlink.</param>
    public Hyperlink(Uri? uri)
    {
        Uri = uri;
    }

    /// <summary>The URL of the hyperlink.</summary>
    public Uri? Uri { get; init; }

    /// <summary>Represents an empty hyperlink.</summary>
    public static readonly Hyperlink None = new(null);

    /// <summary>Gets a value indicating whether the hyperlink is empty.</summary>
    public bool IsEmpty => Uri is null;

    /// <summary>Deconstructs the hyperlink into its Uri.</summary>
    /// <param name="uri">The Uri object.</param>
    public void Deconstruct(out Uri? uri)
    {
        uri = Uri;
    }
}

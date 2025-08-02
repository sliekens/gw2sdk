namespace GuildWars2;

/// <summary>Represents a hyperlink.</summary>
/// <remarks>Not meant to be used directly. A hyperlink is usually obtained from an API response. For example, to link to
/// related resources.</remarks>
[PublicAPI]
public readonly record struct Hyperlink
{
    /// <summary>Initializes a new instance of the <see cref="Hyperlink"/> struct.</summary>
    /// <param name="url">The URL of the hyperlink.</param>
    [Obsolete("Use the Uri overload instead.")]
    public Hyperlink(string url)
    {
        Url = url;
        Uri = new Uri(url, UriKind.RelativeOrAbsolute);
    }

    /// <summary>Initializes a new instance of the <see cref="Hyperlink"/> struct.</summary>
    /// <param name="uri">The URL of the hyperlink.</param>
    public Hyperlink(Uri? uri)
    {
#pragma warning disable CS0618 // Suppress obsolete warning for Url property
        Url = uri?.ToString() ?? "";
#pragma warning restore CS0618
        Uri = uri;
    }

#pragma warning disable CA1056 // URI-like properties should not be strings
    /// <summary>The URL of the hyperlink.</summary>
    [Obsolete("Use the Uri property instead.")]
    public string Url { get; init; }
#pragma warning restore CA1056 // URI-like properties should not be strings

    /// <summary>The URL of the hyperlink.</summary>
    public Uri? Uri { get; init; }

    /// <summary>Represents an empty hyperlink.</summary>
    public static readonly Hyperlink None = new((Uri?)null);

    /// <summary>Gets a value indicating whether the hyperlink is empty.</summary>
    public bool IsEmpty => Uri is null;

    /// <summary>Deconstructs the hyperlink into its Url string.</summary>
    /// <param name="url">The URL string.</param>
    public void Deconstruct(out string url)
    {
#pragma warning disable CS0618 // Suppress obsolete warning for Url property
        url = Url;
#pragma warning restore CS0618
    }

    /// <summary>Deconstructs the hyperlink into its Uri.</summary>
    /// <param name="uri">The Uri object.</param>
    public void Deconstruct(out Uri? uri)
    {
        uri = Uri;
    }
}

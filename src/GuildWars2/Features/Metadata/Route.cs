namespace GuildWars2.Metadata;

/// <summary>Information about a route in the API.</summary>
[DataTransferObject]
public sealed record Route
{
    /// <summary>The path of the route.</summary>
    public required string Path { get; init; }

    /// <summary>Indicates whether the route supports the 'lang' parameter, or the 'Accept-Language' header.</summary>
    public required bool Multilingual { get; init; }

    /// <summary>Indicates whether the route requires an API key.</summary>
    public required bool RequiresAuthorization { get; init; }

    /// <summary>Indicates whether the route is active.</summary>
    public required bool Active { get; init; }
}

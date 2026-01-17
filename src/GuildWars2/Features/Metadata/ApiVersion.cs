namespace GuildWars2.Metadata;

/// <summary>Information about an API version.</summary>
/// <remarks>Don't expect new top-level versions in the future: '/v1' has been turned off, and '/v2' has been updated with
/// schema versions.</remarks>
[DataTransferObject]
public sealed record ApiVersion
{
    /// <summary>The supported languages for this version of the API.</summary>
    public required IImmutableValueList<string> Languages { get; init; }

    /// <summary>The routes available in this version of the API.</summary>
    public required IImmutableValueList<Route> Routes { get; init; }

    /// <summary>The schema versions available in this version of the API.</summary>
    public required IImmutableValueList<Schema> SchemaVersions { get; init; }
}

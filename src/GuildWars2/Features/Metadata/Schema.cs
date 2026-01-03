namespace GuildWars2.Metadata;

/// <summary>Information about a schema version.</summary>
[DataTransferObject]
public sealed record Schema
{
    /// <summary>The version of the schema.</summary>
    public required string Version { get; init; }

    /// <summary>The release notes for the schema version.</summary>
    public required string Description { get; init; }
}

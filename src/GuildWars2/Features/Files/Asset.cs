namespace GuildWars2.Files;

/// <summary>Information about a file.</summary>
[DataTransferObject]
public sealed record Asset
{
    /// <summary>The file ID.</summary>
    public required string Id { get; init; }

    /// <summary>The file URL.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The file URL.</summary>
    public required Uri IconUrl { get; init; }
}

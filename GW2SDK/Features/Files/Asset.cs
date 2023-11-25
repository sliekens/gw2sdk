namespace GuildWars2.Files;

/// <summary>
/// Information about a file.
/// </summary>
[PublicAPI]
[DataTransferObject]
public sealed record Asset

{
    /// <summary>
    /// The file ID.
    /// </summary>
    public required string Id { get; init; }

    public required string IconHref { get; init; }
}

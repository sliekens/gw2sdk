namespace GuildWars2.Metadata;

/// <summary>Information about the current build of the game client.</summary>
[DataTransferObject]
public sealed record Build
{
    /// <summary>The latest build ID of the game client.</summary>
    public required int Id { get; init; }
}

namespace GuildWars2.Guilds.Emblems;

/// <summary>Information about the guild's current emblem background.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GuildEmblemBackground
{
    /// <summary>The background ID.</summary>
    public required int Id { get; init; }

    /// <summary>The color IDs of the background layers.</summary>
    public required IReadOnlyList<int> ColorIds { get; init; }
}

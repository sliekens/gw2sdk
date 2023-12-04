﻿namespace GuildWars2.Guilds.Emblems;

/// <summary>Information about the guild's current emblem foreground.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GuildEmblemForeground
{
    /// <summary>The foreground ID.</summary>
    public required int Id { get; init; }

    /// <summary>The color IDs of the foreground layers.</summary>
    public required IReadOnlyList<int> ColorIds { get; init; }
}

﻿namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record RankTier
{
    public required int Rating { get; init; }
}

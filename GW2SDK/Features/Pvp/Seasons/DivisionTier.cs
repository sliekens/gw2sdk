﻿namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record DivisionTier
{
    public required int Points { get; init; }
}

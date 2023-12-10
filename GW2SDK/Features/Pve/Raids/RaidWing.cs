﻿namespace GuildWars2.Pve.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record RaidWing
{
    public required string Id { get; init; }

    public required IReadOnlyList<Encounter> Encounters { get; init; }
}
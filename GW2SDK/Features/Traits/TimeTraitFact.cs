﻿namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record TimeTraitFact : TraitFact
{
    public required TimeSpan Duration { get; init; }
}

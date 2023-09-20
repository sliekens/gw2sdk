﻿namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record RangeTraitFact : TraitFact
{
    public required int Value { get; init; }
}

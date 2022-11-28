﻿using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record DistanceTraitFact : TraitFact
{
    public required int Distance { get; init; }
}

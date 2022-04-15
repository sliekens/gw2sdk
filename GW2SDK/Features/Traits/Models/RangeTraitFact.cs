﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits.Models;

[PublicAPI]
[DataTransferObject]
public sealed record RangeTraitFact : TraitFact
{
    public int Value { get; init; }
}

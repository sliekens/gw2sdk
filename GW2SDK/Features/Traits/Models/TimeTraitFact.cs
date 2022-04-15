using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits.Models;

[PublicAPI]
[DataTransferObject]
public sealed record TimeTraitFact : TraitFact
{
    public TimeSpan Duration { get; init; }
}

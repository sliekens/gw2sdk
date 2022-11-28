using System;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record TimeTraitFact : TraitFact
{
    public required TimeSpan Duration { get; init; }
}

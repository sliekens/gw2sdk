using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record DamageTraitFact : TraitFact
{
    public required int HitCount { get; init; }

    public required double DamageMultiplier { get; init; }
}

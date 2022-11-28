using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record RechargeTraitFact : TraitFact
{
    public required double Value { get; init; }
}

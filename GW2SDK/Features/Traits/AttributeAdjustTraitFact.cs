using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record AttributeAdjustTraitFact : TraitFact
{
    public required int Value { get; init; }

    public required AttributeAdjustTarget Target { get; init; }
}

using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record AttributeAdjustTraitFact : TraitFact
{
    public int Value { get; init; }

    public AttributeAdjustTarget Target { get; init; }
}

using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record AttributeAdjustTraitFact : TraitFact
{
    public required int Value { get; init; }

    public required AttributeAdjustTarget Target { get; init; }
}

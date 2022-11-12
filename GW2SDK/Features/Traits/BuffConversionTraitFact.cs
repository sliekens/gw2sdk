using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record BuffConversionTraitFact : TraitFact
{
    public required int Percent { get; init; }

    public required AttributeAdjustTarget Source { get; init; }

    public required AttributeAdjustTarget Target { get; init; }
}

using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits.Models;

[PublicAPI]
[DataTransferObject]
public sealed record BuffConversionTraitFact : TraitFact
{
    public int Percent { get; init; }

    public AttributeAdjustTarget Source { get; init; }

    public AttributeAdjustTarget Target { get; init; }
}

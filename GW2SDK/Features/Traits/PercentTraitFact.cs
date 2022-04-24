using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record PercentTraitFact : TraitFact
{
    public double Percent { get; init; }
}

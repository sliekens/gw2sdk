using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record DistanceTraitFact : TraitFact
{
    public required int Distance { get; init; }
}

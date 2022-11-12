using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record RechargeTraitFact : TraitFact
{
    public required double Value { get; init; }
}

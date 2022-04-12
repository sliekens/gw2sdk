using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record RechargeTraitFact : TraitFact
{
    public double Value { get; init; }
}
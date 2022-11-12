using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record UnblockableTraitFact : TraitFact
{
    public required bool Value { get; init; }
}

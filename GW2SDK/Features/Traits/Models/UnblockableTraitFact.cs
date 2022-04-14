using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Traits.Models;

[PublicAPI]
[DataTransferObject]
public sealed record UnblockableTraitFact : TraitFact
{
    public bool Value { get; init; }
}
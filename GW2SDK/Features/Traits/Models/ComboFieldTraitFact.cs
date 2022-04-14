using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Traits.Models;

[PublicAPI]
[DataTransferObject]
public sealed record ComboFieldTraitFact : TraitFact
{
    public ComboFieldName Field { get; init; }
}
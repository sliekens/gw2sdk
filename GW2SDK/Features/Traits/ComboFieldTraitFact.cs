using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record ComboFieldTraitFact : TraitFact
{
    public required ComboFieldName Field { get; init; }
}

using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record ComboFieldTraitFact : TraitFact
{
    public required ComboFieldName Field { get; init; }
}

using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record UnblockableTraitFact : TraitFact
{
    public required bool Value { get; init; }
}

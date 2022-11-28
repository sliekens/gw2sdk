using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record PrefixedBuffTraitFact : BuffTraitFact
{
    public required BuffPrefix Prefix { get; init; }
}

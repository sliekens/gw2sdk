using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record PrefixedBuffTraitFact : BuffTraitFact
{
    public required BuffPrefix Prefix { get; init; }
}

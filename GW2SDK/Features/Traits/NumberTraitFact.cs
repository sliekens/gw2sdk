using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
public sealed record NumberTraitFact : TraitFact
{
    public required int Value { get; init; }
}

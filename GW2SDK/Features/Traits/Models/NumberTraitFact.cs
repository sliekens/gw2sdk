using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
public sealed record NumberTraitFact : TraitFact
{
    public int Value { get; init; }
}
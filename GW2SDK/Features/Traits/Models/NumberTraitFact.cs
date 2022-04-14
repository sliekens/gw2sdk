using JetBrains.Annotations;

namespace GW2SDK.Traits.Models;

[PublicAPI]
public sealed record NumberTraitFact : TraitFact
{
    public int Value { get; init; }
}
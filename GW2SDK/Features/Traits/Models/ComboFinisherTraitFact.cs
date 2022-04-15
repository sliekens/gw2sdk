using JetBrains.Annotations;

namespace GW2SDK.Traits.Models;

[PublicAPI]
public sealed record ComboFinisherTraitFact : TraitFact
{
    public int Percent { get; init; }

    public ComboFinisherName FinisherName { get; init; }
}

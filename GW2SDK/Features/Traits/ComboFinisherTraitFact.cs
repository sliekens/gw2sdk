using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
public sealed record ComboFinisherTraitFact : TraitFact
{
    public required int Percent { get; init; }

    public required ComboFinisherName FinisherName { get; init; }
}

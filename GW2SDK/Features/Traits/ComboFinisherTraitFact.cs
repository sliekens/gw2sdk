using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
public sealed record ComboFinisherTraitFact : TraitFact
{
    public required int Percent { get; init; }

    public required ComboFinisherName FinisherName { get; init; }
}

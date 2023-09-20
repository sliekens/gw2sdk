namespace GuildWars2.Traits;

[PublicAPI]
public sealed record NumberTraitFact : TraitFact
{
    public required int Value { get; init; }
}

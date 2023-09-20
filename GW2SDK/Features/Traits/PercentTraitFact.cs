namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record PercentTraitFact : TraitFact
{
    public required double Percent { get; init; }
}

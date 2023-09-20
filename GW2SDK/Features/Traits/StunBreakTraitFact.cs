namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record StunBreakTraitFact : TraitFact
{
    public required bool Value { get; init; }
}

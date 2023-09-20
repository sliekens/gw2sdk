namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record RechargeTraitFact : TraitFact
{
    public required double Value { get; init; }
}

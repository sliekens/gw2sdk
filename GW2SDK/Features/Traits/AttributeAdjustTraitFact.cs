namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record AttributeAdjustTraitFact : TraitFact
{
    public required int Value { get; init; }

    public required AttributeAdjustmentTarget Target { get; init; }
}

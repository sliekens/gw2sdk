namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record BuffConversionTraitFact : TraitFact
{
    public required int Percent { get; init; }

    public required AttributeAdjustmentTarget Source { get; init; }

    public required AttributeAdjustmentTarget Target { get; init; }
}

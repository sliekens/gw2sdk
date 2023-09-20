namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record BuffConversionTraitFact : TraitFact
{
    public required int Percent { get; init; }

    public required AttributeAdjustTarget Source { get; init; }

    public required AttributeAdjustTarget Target { get; init; }
}

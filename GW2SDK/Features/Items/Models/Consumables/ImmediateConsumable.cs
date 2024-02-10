namespace GuildWars2.Items;

[PublicAPI]
public sealed record ImmediateConsumable : Consumable
{
    public required TimeSpan? Duration { get; init; }

    public required int? ApplyCount { get; init; }

    public required string EffectName { get; init; }

    public required string EffectDescription { get; init; }

    /// <summary>The URL of the consumable effect icon.</summary>
    public required string? EffectIconHref { get; init; }

    public required int? GuildUpgradeId { get; init; }
}

namespace GuildWars2.Items;

[PublicAPI]
public sealed record Food : Consumable
{
    public required TimeSpan? Duration { get; init; }

    public required int? ApplyCount { get; init; }

    public required string EffectName { get; init; }

    public required string EffectDescription { get; init; }

    /// <summary>The URL of the food effect icon.</summary>
    public required string? EffectIconHref { get; init; }
}

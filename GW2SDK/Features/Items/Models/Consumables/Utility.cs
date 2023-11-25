namespace GuildWars2.Items;

[PublicAPI]
public sealed record Utility : Consumable
{
    public required TimeSpan? Duration { get; init; }

    public required int? ApplyCount { get; init; }

    public required string EffectName { get; init; }

    public required string EffectDescription { get; init; }

    public required string? EffectIconHref { get; init; }
}

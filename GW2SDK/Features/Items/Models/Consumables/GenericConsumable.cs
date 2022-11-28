using System;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record GenericConsumable : Consumable
{
    public required TimeSpan? Duration { get; init; }

    public required int? ApplyCount { get; init; }

    public required string EffectName { get; init; }

    public required string EffectDescription { get; init; }

    public required string? EffectIcon { get; init; }

    public required int? GuildUpgradeId { get; init; }
}

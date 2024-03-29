﻿namespace GuildWars2.Hero.Achievements;

/// <summary>An item reward for completing an achievement.</summary>
[PublicAPI]
public sealed record ItemReward : AchievementReward
{
    /// <summary>The item ID of the reward.</summary>
    public required int Id { get; init; }

    /// <summary>The amount of items received.</summary>
    public required int Count { get; init; }
}

﻿namespace GuildWars2.Crafting;

[PublicAPI]
public sealed record GuildConsumableWvwRecipe : Recipe
{
    public required int? OutputUpgradeId { get; init; }
}
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a guild consumable, for example a banner.</summary>
[JsonConverter(typeof(GuildConsumableRecipeJsonConverter))]
public sealed record GuildConsumableRecipe : Recipe
{
    /// <summary>The ingredients from guild storage required to craft the consumable.</summary>
    public required IImmutableValueList<GuildIngredient> GuildIngredients { get; init; }

    /// <summary>The guild upgrade ID of the crafted consumable.</summary>
    public required int OutputUpgradeId { get; init; }
}

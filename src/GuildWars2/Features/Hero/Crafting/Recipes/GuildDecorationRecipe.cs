using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a guild decoration.</summary>
[JsonConverter(typeof(GuildDecorationRecipeJsonConverter))]
public sealed record GuildDecorationRecipe : Recipe
{
    /// <summary>The ingredients from guild storage required to craft the decoration.</summary>
    public required IReadOnlyList<GuildIngredient> GuildIngredients { get; init; }

    /// <summary>The guild upgrade ID of the crafted decoration.</summary>
    public required int OutputUpgradeId { get; init; }
}

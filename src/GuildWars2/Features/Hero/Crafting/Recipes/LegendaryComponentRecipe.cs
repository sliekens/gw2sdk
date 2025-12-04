using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a component of a legendary weapon crafting collection.</summary>
[JsonConverter(typeof(LegendaryComponentRecipeJsonConverter))]
public sealed record LegendaryComponentRecipe : Recipe;

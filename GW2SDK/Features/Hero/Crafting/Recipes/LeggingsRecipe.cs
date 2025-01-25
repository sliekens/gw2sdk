using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting leggings.</summary>
[PublicAPI]
[JsonConverter(typeof(LeggingsRecipeJsonConverter))]
public sealed record LeggingsRecipe : Recipe;

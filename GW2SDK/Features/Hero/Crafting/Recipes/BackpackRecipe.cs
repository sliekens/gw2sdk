using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a backpack.</summary>
[PublicAPI]
[JsonConverter(typeof(BackpackRecipeJsonConverter))]
public sealed record BackpackRecipe : Recipe;

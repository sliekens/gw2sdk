using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting seasoning.</summary>
[JsonConverter(typeof(SeasoningRecipeJsonConverter))]
public sealed record SeasoningRecipe : Recipe;

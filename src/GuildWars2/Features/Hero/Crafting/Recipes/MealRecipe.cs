using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for cooking meals.</summary>
[JsonConverter(typeof(MealRecipeJsonConverter))]
public sealed record MealRecipe : Recipe;

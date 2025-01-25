using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a cooking ingredient, for example a Jar of Tomato Sauce.</summary>
[PublicAPI]
[JsonConverter(typeof(IngredientCookingRecipeJsonConverter))]
public sealed record IngredientCookingRecipe : Recipe;

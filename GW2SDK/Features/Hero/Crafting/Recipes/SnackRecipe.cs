using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for cooking snacks.</summary>
[PublicAPI]
[JsonConverter(typeof(SnackRecipeJsonConverter))]
public sealed record SnackRecipe : Recipe;

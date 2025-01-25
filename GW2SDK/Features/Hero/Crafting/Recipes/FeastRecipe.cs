using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a feast.</summary>
[PublicAPI]
[JsonConverter(typeof(FeastRecipeJsonConverter))]
public sealed record FeastRecipe : Recipe;

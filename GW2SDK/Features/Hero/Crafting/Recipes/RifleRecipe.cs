using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a rifle.</summary>
[PublicAPI]
[JsonConverter(typeof(RifleRecipeJsonConverter))]
public sealed record RifleRecipe : Recipe;

using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for cooking soups.</summary>
[PublicAPI]
[JsonConverter(typeof(SoupRecipeJsonConverter))]
public sealed record SoupRecipe : Recipe;

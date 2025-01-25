using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a longbow.</summary>
[PublicAPI]
[JsonConverter(typeof(LongbowRecipeJsonConverter))]
public sealed record LongbowRecipe : Recipe;

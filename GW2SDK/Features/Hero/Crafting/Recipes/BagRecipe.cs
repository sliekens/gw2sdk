using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a bag.</summary>
[PublicAPI]
[JsonConverter(typeof(BagRecipeJsonConverter))]
public sealed record BagRecipe : Recipe;

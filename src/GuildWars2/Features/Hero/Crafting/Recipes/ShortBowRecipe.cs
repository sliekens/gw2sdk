using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a short bow.</summary>
[JsonConverter(typeof(ShortBowRecipeJsonConverter))]
public sealed record ShortBowRecipe : Recipe;

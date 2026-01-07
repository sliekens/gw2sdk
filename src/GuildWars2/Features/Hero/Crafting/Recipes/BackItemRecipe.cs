using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a back item.</summary>
[JsonConverter(typeof(BackItemRecipeJsonConverter))]
public sealed record BackItemRecipe : Recipe;

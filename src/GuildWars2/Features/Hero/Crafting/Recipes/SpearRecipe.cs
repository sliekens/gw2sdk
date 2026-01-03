using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a spear.</summary>
[JsonConverter(typeof(SpearRecipeJsonConverter))]
public sealed record SpearRecipe : Recipe;

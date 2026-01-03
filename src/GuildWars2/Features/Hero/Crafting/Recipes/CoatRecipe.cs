using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a coat.</summary>
[JsonConverter(typeof(CoatRecipeJsonConverter))]
public sealed record CoatRecipe : Recipe;

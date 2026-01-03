using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting an axe.</summary>
[JsonConverter(typeof(AxeRecipeJsonConverter))]
public sealed record AxeRecipe : Recipe;

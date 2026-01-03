using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a focus.</summary>
[JsonConverter(typeof(FocusRecipeJsonConverter))]
public sealed record FocusRecipe : Recipe;

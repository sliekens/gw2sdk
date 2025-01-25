using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a sword.</summary>
[PublicAPI]
[JsonConverter(typeof(SwordRecipeJsonConverter))]
public sealed record SwordRecipe : Recipe;

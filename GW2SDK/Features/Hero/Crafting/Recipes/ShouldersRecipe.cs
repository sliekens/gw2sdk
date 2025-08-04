using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting shoulders.</summary>
[JsonConverter(typeof(ShouldersRecipeJsonConverter))]
public sealed record ShouldersRecipe : Recipe;

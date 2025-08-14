using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting an amulet.</summary>
[JsonConverter(typeof(AmuletRecipeJsonConverter))]
public sealed record AmuletRecipe : Recipe;

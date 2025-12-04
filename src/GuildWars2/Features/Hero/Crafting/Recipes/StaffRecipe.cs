using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a staff.</summary>
[JsonConverter(typeof(StaffRecipeJsonConverter))]
public sealed record StaffRecipe : Recipe;

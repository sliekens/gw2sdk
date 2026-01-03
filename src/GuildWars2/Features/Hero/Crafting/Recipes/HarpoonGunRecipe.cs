using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a harpoon gun.</summary>
[JsonConverter(typeof(HarpoonGunRecipeJsonConverter))]
public sealed record HarpoonGunRecipe : Recipe;

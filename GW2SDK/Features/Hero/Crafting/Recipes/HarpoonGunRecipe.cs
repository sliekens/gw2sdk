using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a harpoon gun.</summary>
[PublicAPI]
[JsonConverter(typeof(HarpoonGunRecipeJsonConverter))]
public sealed record HarpoonGunRecipe : Recipe;

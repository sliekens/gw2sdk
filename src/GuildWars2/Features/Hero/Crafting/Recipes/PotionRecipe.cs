using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a potion.</summary>
[JsonConverter(typeof(PotionRecipeJsonConverter))]
public sealed record PotionRecipe : Recipe;

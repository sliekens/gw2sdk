using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting boots.</summary>
[JsonConverter(typeof(BootsRecipeJsonConverter))]
public sealed record BootsRecipe : Recipe;

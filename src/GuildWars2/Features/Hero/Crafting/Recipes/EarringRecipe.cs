using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting an earring.</summary>
[JsonConverter(typeof(EarringRecipeJsonConverter))]
public sealed record EarringRecipe : Recipe;

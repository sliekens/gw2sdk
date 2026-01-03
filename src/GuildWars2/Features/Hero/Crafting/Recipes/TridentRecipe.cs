using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a trident.</summary>
[JsonConverter(typeof(TridentRecipeJsonConverter))]
public sealed record TridentRecipe : Recipe;

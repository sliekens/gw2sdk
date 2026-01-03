using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a torch.</summary>
[JsonConverter(typeof(TorchRecipeJsonConverter))]
public sealed record TorchRecipe : Recipe;

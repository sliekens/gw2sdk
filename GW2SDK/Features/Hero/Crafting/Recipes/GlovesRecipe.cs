using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting gloves.</summary>
[PublicAPI]
[JsonConverter(typeof(GlovesRecipeJsonConverter))]
public sealed record GlovesRecipe : Recipe;

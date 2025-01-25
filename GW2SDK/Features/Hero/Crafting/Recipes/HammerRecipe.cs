using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a hammer.</summary>
[PublicAPI]
[JsonConverter(typeof(HammerRecipeJsonConverter))]
public sealed record HammerRecipe : Recipe;

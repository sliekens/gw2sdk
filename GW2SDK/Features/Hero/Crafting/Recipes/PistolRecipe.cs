using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a pistol.</summary>
[JsonConverter(typeof(PistolRecipeJsonConverter))]
public sealed record PistolRecipe : Recipe;

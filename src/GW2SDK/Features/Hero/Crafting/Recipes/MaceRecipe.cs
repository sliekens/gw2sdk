using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a mace.</summary>
[JsonConverter(typeof(MaceRecipeJsonConverter))]
public sealed record MaceRecipe : Recipe;

using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a dagger.</summary>
[JsonConverter(typeof(DaggerRecipeJsonConverter))]
public sealed record DaggerRecipe : Recipe;

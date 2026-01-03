using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a ring.</summary>
[JsonConverter(typeof(RingRecipeJsonConverter))]
public sealed record RingRecipe : Recipe;

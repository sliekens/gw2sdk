using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for refining materials.</summary>
[PublicAPI]
[JsonConverter(typeof(RefinementRecipeJsonConverter))]
public sealed record RefinementRecipe : Recipe;

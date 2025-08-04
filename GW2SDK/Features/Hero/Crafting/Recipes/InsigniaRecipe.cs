using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting insignias used in armor crafting.</summary>
[JsonConverter(typeof(InsigniaRecipeJsonConverter))]
public sealed record InsigniaRecipe : Recipe;

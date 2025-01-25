using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting headgear.</summary>
[PublicAPI]
[JsonConverter(typeof(HeadgearRecipeJsonConverter))]
public sealed record HeadgearRecipe : Recipe;

using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting headgear.</summary>
[JsonConverter(typeof(HeadgearRecipeJsonConverter))]
public sealed record HeadgearRecipe : Recipe;

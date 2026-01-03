using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a greatsword.</summary>
[JsonConverter(typeof(GreatswordRecipeJsonConverter))]
public sealed record GreatswordRecipe : Recipe;

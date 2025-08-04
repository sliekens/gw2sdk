using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a dessert, for example Pumpkin Pie.</summary>
[JsonConverter(typeof(DessertRecipeJsonConverter))]
public sealed record DessertRecipe : Recipe;

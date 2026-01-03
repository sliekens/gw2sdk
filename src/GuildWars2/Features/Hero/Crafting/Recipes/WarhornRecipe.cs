using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a warhorn.</summary>
[JsonConverter(typeof(WarhornRecipeJsonConverter))]
public sealed record WarhornRecipe : Recipe;

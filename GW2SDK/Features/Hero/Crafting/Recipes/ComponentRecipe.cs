using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a crafting component, for example a Vision Crystal.</summary>
[JsonConverter(typeof(ComponentRecipeJsonConverter))]
public sealed record ComponentRecipe : Recipe;

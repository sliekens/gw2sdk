using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a shield.</summary>
[PublicAPI]
[JsonConverter(typeof(ShieldRecipeJsonConverter))]
public sealed record ShieldRecipe : Recipe;

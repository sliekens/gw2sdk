using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a dye.</summary>
[PublicAPI]
[JsonConverter(typeof(DyeRecipeJsonConverter))]
public sealed record DyeRecipe : Recipe;

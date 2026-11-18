using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a consumable item, for example sharpening stones, tonics, metabolic
/// primers, varietal seed pouches.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record ConsumableRecipe : Recipe;

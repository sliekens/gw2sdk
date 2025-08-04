using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting an ascended material that requires Obsidian Shards like a Bloodstone
/// Brick.</summary>
[JsonConverter(typeof(RefinementObsidianRecipeJsonConverter))]
public sealed record RefinementObsidianRecipe : Recipe;

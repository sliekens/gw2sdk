using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for bulk crafting.</summary>
[PublicAPI]
[JsonConverter(typeof(BulkRecipeJsonConverter))]
public sealed record BulkRecipe : Recipe;

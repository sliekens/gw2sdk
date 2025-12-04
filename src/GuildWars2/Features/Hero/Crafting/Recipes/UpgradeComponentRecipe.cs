using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting an upgrade component.</summary>
[JsonConverter(typeof(UpgradeComponentRecipeJsonConverter))]
public sealed record UpgradeComponentRecipe : Recipe;

using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting inscriptions used in weapon crafting.</summary>
[JsonConverter(typeof(InscriptionRecipeJsonConverter))]
public sealed record InscriptionRecipe : Recipe;

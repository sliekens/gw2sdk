using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting an ascended material that requires Globs of Ectoplasm like a Lump of
/// Mithrillium.</summary>
[PublicAPI]
[JsonConverter(typeof(RefinementEctoplasmRecipeJsonConverter))]
public sealed record RefinementEctoplasmRecipe : Recipe;

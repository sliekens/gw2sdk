﻿using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a shortbow.</summary>
[PublicAPI]
[JsonConverter(typeof(ShortbowRecipeJsonConverter))]
public sealed record ShortbowRecipe : Recipe;

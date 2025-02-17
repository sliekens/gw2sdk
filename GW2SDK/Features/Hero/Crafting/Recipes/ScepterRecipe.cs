﻿using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a scepter.</summary>
[PublicAPI]
[JsonConverter(typeof(ScepterRecipeJsonConverter))]
public sealed record ScepterRecipe : Recipe;

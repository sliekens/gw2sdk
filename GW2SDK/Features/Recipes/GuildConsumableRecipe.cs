﻿using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class GuildConsumableRecipe : Recipe
    {
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public GuildIngredient[]? GuildIngredients { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int OutputUpgradeId { get; set; }
    }
}

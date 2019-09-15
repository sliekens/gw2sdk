using System;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Recipes.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(RecipeDiscriminatorOptions))]
    public class Recipe
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int OutputItemId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int OutputItemCount { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MinRating { get; set; }

        [JsonProperty("time_to_craft_ms", Required = Required.Always)]
        [JsonConverter(typeof(MillisecondsConverter))]
        public TimeSpan TimeToCraft { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public CraftingDiscipline[] Disciplines { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public RecipeFlag[] Flags { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Ingredient[] Ingredients { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; }
    }
}

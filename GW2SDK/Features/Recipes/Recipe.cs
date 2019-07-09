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
        public int Id { get; set; }

        public int OutputItemId { get; set; }

        public int OutputItemCount { get; set; }

        public int MinRating { get; set; }

        [JsonProperty("time_to_craft_ms")]
        [JsonConverter(typeof(MillisecondsConverter))]
        public TimeSpan TimeToCraft { get; set; }

        [NotNull]
        public CraftingDiscipline[] Disciplines { get; set; }

        [NotNull]
        public RecipeFlag[] Flags { get; set; }

        [NotNull]
        public Ingredient[] Ingredients { get; set; }

        [NotNull]
        public string ChatLink { get; set; }
    }
}

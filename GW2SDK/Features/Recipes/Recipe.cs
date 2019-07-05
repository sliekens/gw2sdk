using System;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Recipes;
using Newtonsoft.Json;

namespace GW2SDK.Features.Recipes
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
        public Discipline[] Disciplines { get; set; }

        [NotNull]
        public RecipeFlag[] Flags { get; set; }

        [NotNull]
        public Ingredient[] Ingredients { get; set; }

        [NotNull]
        public string ChatLink { get; set; }
    }
}

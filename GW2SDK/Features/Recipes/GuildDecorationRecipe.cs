using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class GuildDecorationRecipe : Recipe
    {
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public GuildIngredient[] GuildIngredients { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int OutputUpgradeId { get; set; }
    }
}

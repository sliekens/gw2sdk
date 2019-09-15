using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class GuildConsumableWvwRecipe : Recipe
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public int? OutputUpgradeId { get; set; }
    }
}

using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class GuildIngredient
    {
        [JsonProperty(Required = Required.Always)]
        public int UpgradeId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Count { get; set; }
    }
}

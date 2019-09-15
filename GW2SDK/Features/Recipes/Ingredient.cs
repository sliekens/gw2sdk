using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class Ingredient
    {
        [JsonProperty(Required = Required.Always)]
        public int ItemId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Count { get; set; }
    }
}

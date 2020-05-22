using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class CraftingRecipeUnlocker : Unlocker
    {
        [JsonProperty(Required = Required.Always)]
        public int RecipeId { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int[]? ExtraRecipeIds { get; set; }
    }
}

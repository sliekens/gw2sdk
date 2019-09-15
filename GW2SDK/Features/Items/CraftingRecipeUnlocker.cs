using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class CraftingRecipeUnlocker : Unlocker
    {
        [JsonProperty(Required = Required.Always)]
        public int RecipeId { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public int[] ExtraRecipeIds { get; set; }
    }
}

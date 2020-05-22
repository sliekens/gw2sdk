using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DyeSlotOverrideInfo
    {
        // BUG: the SnakeCaseNamingStrategy that we use breaks serializing PascalCase property names
        // The easiest workaround is to just specify the name of each JsonProperty
        // More info: https://github.com/arenanet/api-cdi/issues/658

        [JsonProperty(nameof(AsuraFemale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? AsuraFemale { get; set; }

        [JsonProperty(nameof(AsuraMale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? AsuraMale { get; set; }

        [JsonProperty(nameof(CharrFemale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? CharrFemale { get; set; }

        [JsonProperty(nameof(CharrMale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? CharrMale { get; set; }

        [JsonProperty(nameof(HumanFemale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? HumanFemale { get; set; }

        [JsonProperty(nameof(HumanMale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? HumanMale { get; set; }

        [JsonProperty(nameof(NornFemale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? NornFemale { get; set; }

        [JsonProperty(nameof(NornMale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? NornMale { get; set; }

        [JsonProperty(nameof(SylvariFemale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? SylvariFemale { get; set; }

        [JsonProperty(nameof(SylvariMale), Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlot[]? SylvariMale { get; set; }
    }
}

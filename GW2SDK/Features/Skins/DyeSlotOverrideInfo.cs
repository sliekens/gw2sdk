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

        [CanBeNull]
        [JsonProperty(nameof(AsuraFemale))]
        public DyeSlot[] AsuraFemale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(AsuraMale))]
        public DyeSlot[] AsuraMale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(CharrFemale))]
        public DyeSlot[] CharrFemale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(CharrMale))]
        public DyeSlot[] CharrMale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(HumanFemale))]
        public DyeSlot[] HumanFemale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(HumanMale))]
        public DyeSlot[] HumanMale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(NornFemale))]
        public DyeSlot[] NornFemale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(NornMale))]
        public DyeSlot[] NornMale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(SylvariFemale))]
        public DyeSlot[] SylvariFemale { get; set; }

        [CanBeNull]
        [JsonProperty(nameof(SylvariMale))]
        public DyeSlot[] SylvariMale { get; set; }
    }
}

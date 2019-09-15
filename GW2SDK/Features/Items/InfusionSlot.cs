using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class InfusionSlot
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public InfusionSlotFlag[] Flags { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int? ItemId { get; set; }
    }
}

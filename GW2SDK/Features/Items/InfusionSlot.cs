using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class InfusionSlot
    {
        [JsonProperty(Required = Required.Always)]
        public InfusionSlotFlag[] Flags { get; set; } = new InfusionSlotFlag[0];

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? ItemId { get; set; }
    }
}

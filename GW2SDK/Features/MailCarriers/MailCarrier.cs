using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.MailCarriers
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class MailCarrier
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("unlock_items", Required = Required.Always)]
        public int[] UnlockItems { get; set; } = new int[0];
        
        [JsonProperty("order", Required = Required.Always)]
        public int Order { get; set; }
        
        [JsonProperty("icon", Required = Required.Always)]
        public string Icon { get; set; } = "";
        
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty("flags", Required = Required.Always)]
        public MailCarrierFlag[] flags { get; set; } = new MailCarrierFlag[0];
    }
}

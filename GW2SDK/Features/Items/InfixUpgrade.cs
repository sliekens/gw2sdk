using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class InfixUpgrade
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public UpgradeAttribute[] Attributes { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Buff Buff { get; set; }
    }
}

using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Accounts.Achievements
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class AccountAchievement
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int[]? Bits { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Current { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Max { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool Done { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? Repeated { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Unlocked { get; set; }
    }
}

using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class BankSlot
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Count { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int? Charges { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int? Skin { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int[]? Upgrades { get; set; }

        /// <summary>
        /// Indicates which upgrade slots are in use. (0-based)
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public int[]? UpgradeSlotIndices { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int[]? Infusions { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public ItemBinding Binding { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public string? BoundTo { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public SelectedStat? Stats { get; set; }
    }
}

using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class SelectedModification
    {
        // BUG: the SnakeCaseNamingStrategy that we use breaks serializing PascalCase property names
        // The easiest workaround is to just specify the name of each JsonProperty
        // More info: https://github.com/arenanet/api-cdi/issues/658

        [JsonProperty(nameof(AgonyResistance), Required = Required.DisallowNull)]
        public int? AgonyResistance { get; set; }

        [JsonProperty(nameof(BoonDuration), Required = Required.DisallowNull)]
        public int? BoonDuration { get; set; }

        [JsonProperty(nameof(ConditionDamage), Required = Required.DisallowNull)]
        public int? ConditionDamage { get; set; }

        [JsonProperty(nameof(ConditionDuration), Required = Required.DisallowNull)]
        public int? ConditionDuration { get; set; }

        [JsonProperty(nameof(CritDamage), Required = Required.DisallowNull)]
        public int? CritDamage { get; set; }

        [JsonProperty(nameof(Healing), Required = Required.DisallowNull)]
        public int? Healing { get; set; }

        [JsonProperty(nameof(Power), Required = Required.DisallowNull)]
        public int? Power { get; set; }

        [JsonProperty(nameof(Precision), Required = Required.DisallowNull)]
        public int? Precision { get; set; }

        [JsonProperty(nameof(Toughness), Required = Required.DisallowNull)]
        public int? Toughness { get; set; }

        [JsonProperty(nameof(Vitality), Required = Required.DisallowNull)]
        public int? Vitality { get; set; }
    }
}

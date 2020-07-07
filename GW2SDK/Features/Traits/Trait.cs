using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Trait
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("tier", Required = Required.Always)]
        public int Tier { get; set; }

        [JsonProperty("order", Required = Required.Always)]
        public int Order { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty("description", Required = Required.DisallowNull)]
        public string? Description { get; set; }

        [JsonProperty("slot", Required = Required.Always)]
        public TraitSlot Slot { get; set; }

        [JsonProperty("facts", Required = Required.DisallowNull)]
        public TraitFact[]? Facts { get; set; }

        [JsonProperty("traited_facts", Required = Required.DisallowNull)]
        public TraitFact[]? TraitedFacts { get; set; }

        [JsonProperty("skills", Required = Required.DisallowNull)]
        public TraitSkill[]? Skills { get; set; }

        [JsonProperty("specialization", Required = Required.Always)]
        public int SpezializationId { get; set; }

        [JsonProperty("icon", Required = Required.Always)]
        public string Icon { get; set; } = "";
    }
}

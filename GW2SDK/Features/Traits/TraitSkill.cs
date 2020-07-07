using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class TraitSkill
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty("facts", Required = Required.Always)]
        public TraitFact[] Facts { get; set; } = new TraitFact[0];

        [JsonProperty("traited_facts", Required = Required.DisallowNull)]
        public TraitFact[]? TraitedFacts { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; } = "";

        [JsonProperty("icon", Required = Required.Always)]
        public string Icon { get; set; } = "";

        [JsonProperty("flags", Required = Required.Always)]
        public TraitSkillFlag[] Flags { get; set; } = new TraitSkillFlag[0];

        [JsonProperty("chat_link", Required = Required.Always)]
        public string ChatLink { get; set; } = "";

        [JsonProperty("categories", Required = Required.DisallowNull)]
        public SkillCategoryName[]? Categories { get; set; }
    }
}
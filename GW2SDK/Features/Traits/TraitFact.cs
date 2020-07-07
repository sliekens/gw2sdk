using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    public class TraitFact
    {
        [JsonProperty("text", Required = Required.DisallowNull)]
        public string? Text { get; set; }

        [JsonProperty("icon", Required = Required.Always)]
        public string Icon { get; set; } = "";

        /// <summary>The ID of the trait that activates this trait override.</summary>
        [JsonProperty("requires_trait", Required = Required.DisallowNull)]
        public int? RequiresTrait { get; set; }

        /// <summary>The index of the fact that is replaced by this fact when <see cref="RequiresTrait" /> is also active.</summary>
        [JsonProperty("overrides", Required = Required.DisallowNull)]
        public int? Overrides { get; set; }

        /// <summary>Sometimes used to indicate the life force cost for the Necromancer's Shroud skills.</summary>
        [JsonProperty("percent", Required = Required.DisallowNull)]
        public int? Percent { get; set; }
    }
}
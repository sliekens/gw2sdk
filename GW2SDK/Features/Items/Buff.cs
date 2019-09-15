using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class Buff
    {
        [JsonProperty(Required = Required.Always)]
        public int SkillId { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Description { get; set; }
    }
}

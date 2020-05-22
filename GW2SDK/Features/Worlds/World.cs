using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class World
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public WorldPopulation Population { get; set; }
    }
}

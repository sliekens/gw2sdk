using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    public class TokenInfo
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public Permission[] Permissions { get; set; } = new Permission[0];
    }
}

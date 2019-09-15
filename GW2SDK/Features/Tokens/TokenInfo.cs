using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tokens.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(TokenDiscriminatorOptions))]
    public class TokenInfo
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Permission[] Permissions { get; set; }
    }
}

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
        public string Id { get; set; }

        public string Name { get; set; }

        public Permission[] Permissions { get; set; }
    }
}

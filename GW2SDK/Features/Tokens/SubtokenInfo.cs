using System;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public sealed class SubtokenInfo : TokenInfo
    {
        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset ExpiresAt { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset IssuedAt { get; set; }

        [CanBeNull]
        [ItemNotNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public Uri[] Urls { get; set; }
    }
}

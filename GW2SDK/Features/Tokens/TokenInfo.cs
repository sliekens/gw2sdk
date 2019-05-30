using System;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Tokens;
using Newtonsoft.Json;

namespace GW2SDK.Features.Tokens
{
    [Inheritable]
    [DataTransferObject]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(TokenDiscriminatorOptions))]
    public class TokenInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Permission[] Permissions { get; set; }
    }

    public sealed class ApiKeyInfo : TokenInfo
    {
    }

    public sealed class SubtokenInfo : TokenInfo
    {
        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset IssuedAt { get; set; }

        public Uri[] Urls { get; set; }
    }
}

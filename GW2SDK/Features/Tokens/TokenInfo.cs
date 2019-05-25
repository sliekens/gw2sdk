﻿using GW2SDK.Infrastructure;
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
}

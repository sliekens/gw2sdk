using System;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Tokens
{
    [PublicAPI]
    public sealed class SubtokenInfo : TokenInfo
    {
        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset IssuedAt { get; set; }

        public Uri[] Urls { get; set; }
    }
}
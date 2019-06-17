using System;

namespace GW2SDK.Features.Tokens
{
    public sealed class SubtokenInfo : TokenInfo
    {
        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset IssuedAt { get; set; }

        public Uri[] Urls { get; set; }
    }
}
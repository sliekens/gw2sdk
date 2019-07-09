using System;
using GW2SDK.Annotations;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public sealed class SubtokenInfo : TokenInfo
    {
        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset IssuedAt { get; set; }

        public Uri[] Urls { get; set; }
    }
}

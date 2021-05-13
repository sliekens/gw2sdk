using System;
using JetBrains.Annotations;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public sealed record SubtokenInfo : TokenInfo
    {
        public DateTimeOffset ExpiresAt { get; init; }

        public DateTimeOffset IssuedAt { get; init; }

        public Uri[]? Urls { get; init; }
    }
}

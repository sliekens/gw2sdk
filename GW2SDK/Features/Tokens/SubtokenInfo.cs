using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GuildWars2.Tokens;

[PublicAPI]
public sealed record SubtokenInfo : TokenInfo
{
    public required DateTimeOffset ExpiresAt { get; init; }

    public required DateTimeOffset IssuedAt { get; init; }

    public required IReadOnlyCollection<Uri>? Urls { get; init; }
}

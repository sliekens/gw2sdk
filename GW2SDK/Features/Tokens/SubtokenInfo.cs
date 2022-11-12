using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Tokens;

[PublicAPI]
public sealed record SubtokenInfo : TokenInfo
{
    public required DateTimeOffset ExpiresAt { get; init; }

    public required DateTimeOffset IssuedAt { get; init; }

    public required IReadOnlyCollection<Uri>? Urls { get; init; }
}

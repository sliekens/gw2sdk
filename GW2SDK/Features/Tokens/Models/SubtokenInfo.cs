using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace GW2SDK.Tokens.Models;

[PublicAPI]
public sealed record SubtokenInfo : TokenInfo
{
    public DateTimeOffset ExpiresAt { get; init; }

    public DateTimeOffset IssuedAt { get; init; }

    public IReadOnlyCollection<Uri>? Urls { get; init; }
}
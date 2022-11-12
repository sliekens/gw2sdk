using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Tokens;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TokenInfo
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<Permission> Permissions { get; init; }
}

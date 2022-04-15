using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Tokens.Models;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TokenInfo
{
    public string Id { get; init; } = "";

    public string Name { get; init; } = "";

    public IReadOnlyCollection<Permission> Permissions { get; init; } = Array.Empty<Permission>();
}

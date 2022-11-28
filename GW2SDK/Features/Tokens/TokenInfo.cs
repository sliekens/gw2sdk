using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Tokens;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TokenInfo
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<Permission> Permissions { get; init; }
}

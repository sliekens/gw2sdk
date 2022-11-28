using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Guilds.Permissions;

[PublicAPI]
[DataTransferObject]
public sealed record GuildPermissionSummary
{
    public required GuildPermission Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}

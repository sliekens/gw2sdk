using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Permissions;

[PublicAPI]
[DataTransferObject]
public sealed record GuildPermissionSummary
{
    public required GuildPermission Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}

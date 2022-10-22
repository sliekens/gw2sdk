using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Permissions;

[PublicAPI]
[DataTransferObject]
public sealed record GuildPermissionSummary
{
    public string Id { get; init; } = "";

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";
}

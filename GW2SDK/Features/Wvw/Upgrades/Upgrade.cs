using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
[DataTransferObject]
public sealed record Upgrade
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string IconHref { get; init; }
}

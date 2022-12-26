using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
[DataTransferObject]
public sealed record UpgradeTier
{
    public required string Name { get; init; }

    public required int YaksRequired { get; init; }

    public required IReadOnlyCollection<Upgrade> Upgrades { get; init; }
}

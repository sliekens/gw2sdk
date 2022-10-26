using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Upgrades;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record GuildUpgrade
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public TimeSpan BuildTime { get; init; }

    public string Icon { get; init; } = "";

    public int RequiredLevel { get; init; }

    public int Experience { get; init; }

    public IReadOnlyCollection<int> Prerequisites { get; init; } = Array.Empty<int>();

    public IReadOnlyCollection<GuildUpgradeCost> Costs { get; init; } = Array.Empty<GuildUpgradeCost>();
}

﻿using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record SeasonRank
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Icon { get; init; }

    public required string Overlay { get; init; }

    public required string SmallOverlay { get; init; }

    public required IReadOnlyCollection<RankTier> Tiers { get; init; }
}

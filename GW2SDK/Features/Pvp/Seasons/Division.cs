using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record Division
{
    public required string Name { get; init; }

    public required IReadOnlyCollection<DivisionFlag> Flags { get; init; }

    public required string LargeIcon { get; init; }

    public required string SmallIcon { get; init; }

    public required string PipIcon { get; init; }

    public required IReadOnlyCollection<DivisionTier> Tiers { get; init; }
}

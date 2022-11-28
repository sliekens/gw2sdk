using System.Collections.Generic;
using System.Drawing;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.Sectors;

[PublicAPI]
[DataTransferObject]
public sealed record Sector
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required int Level { get; init; }

    public required PointF Coordinates { get; init; }

    public required IReadOnlyCollection<PointF> Boundaries { get; init; }

    public required string ChatLink { get; init; }
}

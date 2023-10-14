using System.Drawing;

namespace GuildWars2.Exploration.MasteryPoints;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryPoint
{
    public required int Id { get; init; }

    public required MasteryRegionName Region { get; init; }

    public required PointF Coordinates { get; init; }
}

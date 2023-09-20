using System.Drawing;

namespace GuildWars2.Exploration.Hearts;

[PublicAPI]
[DataTransferObject]
public sealed record Heart
{
    public required int Id { get; init; }

    public required string Objective { get; init; }

    public required int Level { get; init; }

    public required PointF Coordinates { get; init; }

    public required IReadOnlyCollection<PointF> Boundaries { get; init; }

    public required string ChatLink { get; init; }
}

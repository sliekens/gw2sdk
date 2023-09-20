using System.Drawing;
using GuildWars2.Exploration.Charts;

namespace GuildWars2.Exploration.Regions;

[PublicAPI]
[DataTransferObject]
public sealed record Region
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required PointF LabelCoordinates { get; init; }

    public required Area ContinentRectangle { get; init; }

    public required Dictionary<int, Chart> Maps { get; init; }
}

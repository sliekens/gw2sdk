using System.Drawing;
using GuildWars2.Exploration.Maps;

namespace GuildWars2.Exploration.Regions;

[PublicAPI]
[DataTransferObject]
public sealed record Region
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required Point LabelCoordinates { get; init; }

    public required Rectangle ContinentRectangle { get; init; }

    public required Dictionary<int, Map> Maps { get; init; }
}

using System.Drawing;
using GuildWars2.Exploration.Adventures;
using GuildWars2.Exploration.GodShrines;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Exploration.HeroChallenges;
using GuildWars2.Exploration.MasteryPoints;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Exploration.Sectors;

namespace GuildWars2.Exploration.Maps;

[PublicAPI]
[DataTransferObject]
public sealed record Map
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required int MinLevel { get; init; }

    public required int MaxLevel { get; init; }

    public required int DefaultFloor { get; init; }

    public required Point? LabelCoordinates { get; init; }

    /// <summary>The dimensions of a map.</summary>
    public required Rectangle MapRectangle { get; init; }


    /// <summary>The dimensions of a map within the continent coordinate system.</summary>
    public required Rectangle ContinentRectangle { get; init; }

    public required Dictionary<int, PointOfInterest> PointsOfInterest { get; init; }

    public required Dictionary<int, Heart> Hearts { get; init; }

    public required IReadOnlyCollection<HeroChallenge> HeroChallenges { get; init; }

    public required Dictionary<int, Sector> Sectors { get; init; }

    public required IReadOnlyCollection<Adventure> Adventures { get; init; }

    public required IReadOnlyCollection<MasteryPoint> MasteryPoints { get; init; }

    public required IReadOnlyCollection<GodShrine>? GodShrines { get; init; }
}

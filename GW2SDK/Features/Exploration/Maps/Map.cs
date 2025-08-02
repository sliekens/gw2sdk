using System.Drawing;

using GuildWars2.Exploration.Adventures;
using GuildWars2.Exploration.GodShrines;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Exploration.HeroChallenges;
using GuildWars2.Exploration.MasteryInsights;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Exploration.Sectors;

namespace GuildWars2.Exploration.Maps;

/// <summary>Information about a map.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Map
{
    /// <summary>The map ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the map.</summary>
    public required string Name { get; init; }

    /// <summary>The minimum player level of the map.</summary>
    public required int MinLevel { get; init; }

    /// <summary>The maximum player level of the map.</summary>
    public required int MaxLevel { get; init; }

    /// <summary>The default floor of the map.</summary>
    public required int DefaultFloor { get; init; }

    /// <summary>The coordinates of the map label.</summary>
    public required Point? LabelCoordinates { get; init; }

    /// <summary>The dimensions of the map.</summary>
    public required Rectangle MapRectangle { get; init; }

    /// <summary>The dimensions of the map within the continent coordinate system.</summary>
    public required Rectangle ContinentRectangle { get; init; }

    /// <summary>The points of interest on the map. The key is the point of interest ID. The value is the point of interest.</summary>
    public required Dictionary<int, PointOfInterest> PointsOfInterest { get; init; }

    /// <summary>The hearts on the map. The key is the heart ID. The value is the heart.</summary>
    public required Dictionary<int, Heart> Hearts { get; init; }

    /// <summary>The hero challenges on the map.</summary>
    public required IReadOnlyCollection<HeroChallenge> HeroChallenges { get; init; }

    /// <summary>The sectors on the map. The key is the sector ID. The value is the sector.</summary>
    public required Dictionary<int, Sector> Sectors { get; init; }

    /// <summary>The adventures on the map.</summary>
    public required IReadOnlyCollection<Adventure> Adventures { get; init; }

    /// <summary>The mastery insights on the map.</summary>
    public required IReadOnlyCollection<MasteryInsight> MasteryInsights { get; init; }

    /// <summary>The god shrines on the map.</summary>
    public required IReadOnlyCollection<GodShrine>? GodShrines { get; init; }
}

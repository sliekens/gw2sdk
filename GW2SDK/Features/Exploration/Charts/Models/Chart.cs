using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using GW2SDK.Exploration.Adventures;
using GW2SDK.Exploration.GodShrines;
using GW2SDK.Exploration.Hearts;
using GW2SDK.Exploration.MasteryPoints;
using GW2SDK.Exploration.PointsOfInterest;
using GW2SDK.Exploration.Sectors;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Charts;

[PublicAPI]
[DataTransferObject]
public sealed record Chart
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required int MinLevel { get; init; }

    public required int MaxLevel { get; init; }

    public required int DefaultFloor { get; init; }

    public required PointF? LabelCoordinates { get; init; }

    public required MapArea ChartRectangle { get; init; }

    public required Area ContinentRectangle { get; init; }

    public required Dictionary<int, PointOfInterest> PointsOfInterest { get; init; }

    public required Dictionary<int, Heart> Hearts { get; init; }

    public required IReadOnlyCollection<SkillChallenge.SkillChallenge> SkillChallenges
    {
        get;
        init;
    }

    public required Dictionary<int, Sector> Sectors { get; init; }

    public required IReadOnlyCollection<Adventure> Adventures { get; init; }

    public required IReadOnlyCollection<MasteryPoint> MasteryPoints { get; init; }

    public required IReadOnlyCollection<GodShrine>? GodShrines { get; init; }
}

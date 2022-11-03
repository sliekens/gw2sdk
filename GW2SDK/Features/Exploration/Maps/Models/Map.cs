using System;
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

namespace GW2SDK.Exploration.Maps;

[PublicAPI]
[DataTransferObject]
public sealed record Map
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public int MinLevel { get; init; }

    public int MaxLevel { get; init; }

    public int DefaultFloor { get; init; }

    public PointF? LabelCoordinates { get; init; }

    public MapArea MapRectangle { get; init; } = new();

    public Area ContinentRectangle { get; init; } = new();

    public Dictionary<int, PointOfInterest> PointsOfInterest { get; init; } = new(0);

    public Dictionary<int, Heart> Hearts { get; init; } = new(0);

    public IReadOnlyCollection<SkillChallenge.SkillChallenge> SkillChallenges { get; init; } =
        Array.Empty<SkillChallenge.SkillChallenge>();

    public Dictionary<int, Sector> Sectors { get; init; } = new(0);

    public IReadOnlyCollection<Adventure> Adventures { get; init; } = Array.Empty<Adventure>();

    public IReadOnlyCollection<MasteryPoint> MasteryPoints { get; init; } =
        Array.Empty<MasteryPoint>();

    public IReadOnlyCollection<GodShrine>? GodShrines { get; init; }
}

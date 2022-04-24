using System;
using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Maps;

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

    public MapRectangle MapRectangle { get; init; } = new();

    public ContinentRectangle ContinentRectangle { get; init; } = new();

    public Dictionary<int, PointOfInterest> PointsOfInterest { get; init; } = new(0);

    public Dictionary<int, MapTask> Tasks { get; init; } = new(0);

    public IReadOnlyCollection<SkillChallenge> SkillChallenges { get; init; } =
        Array.Empty<SkillChallenge>();

    public Dictionary<int, MapSector> Sectors { get; init; } = new(0);

    public IReadOnlyCollection<Adventure> Adventures { get; init; } = Array.Empty<Adventure>();

    public IReadOnlyCollection<MasteryPoint> MasteryPoints { get; init; } =
        Array.Empty<MasteryPoint>();

    public IReadOnlyCollection<GodShrine>? GodShrines { get; init; }
}

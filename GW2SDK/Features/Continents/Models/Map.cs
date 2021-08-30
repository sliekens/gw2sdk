using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Map
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public int MinLevel { get; init; }

        public int MaxLevel { get; init; }

        public int DefaultFloor { get; init; }

        public double[]? LabelCoordinates { get; init; }

        public double[][] MapRectangle { get; init; } = Array.Empty<double[]>();

        public double[][] ContinentRectangle { get; init; } = Array.Empty<double[]>();

        public Dictionary<int, PointOfInterest> PointsOfInterest { get; init; } = new(0);

        public Dictionary<int, MapTask> Tasks { get; init; } = new(0);

        public SkillChallenge[] SkillChallenges { get; init; } = Array.Empty<SkillChallenge>();

        public Dictionary<int, MapSector> Sectors { get; init; } = new(0);

        public Adventure[] Adventures { get; init; } = Array.Empty<Adventure>();

        public MasteryPoint[] MasteryPoints { get; init; } = Array.Empty<MasteryPoint>();

        public GodShrine[]? GodShrines { get; init; }
    }
}

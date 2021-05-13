using System.Collections.Generic;
using GW2SDK.Annotations;

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

        public double[][] MapRectangle { get; init; } = new double[0][];

        public double[][] ContinentRectangle { get; init; } = new double[0][];

        public Dictionary<int, PointOfInterest> PointsOfInterest { get; init; } = new(0);

        public Dictionary<int, MapTask> Tasks { get; init; } = new(0);

        public SkillChallenge[] SkillChallenges { get; init; } = new SkillChallenge[0];

        public Dictionary<int, MapSector> Sectors { get; init; } = new(0);

        public Adventure[] Adventures { get; init; } = new Adventure[0];

        public MasteryPoint[] MasteryPoints { get; init; } = new MasteryPoint[0];

        public GodShrine[]? GodShrines { get; init; }
    }
}

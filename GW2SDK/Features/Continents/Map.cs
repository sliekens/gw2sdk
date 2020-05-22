using System.Collections.Generic;
using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class Map
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public int MinLevel { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MaxLevel { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int DefaultFloor { get; set; }

        [JsonProperty("label_coord", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int[]? LabelCoordinates { get; set; }

        [JsonProperty("map_rect", Required = Required.Always)]
        public int[][] MapRectangle { get; set; } = new int[0][];

        [JsonProperty("continent_rect", Required = Required.Always)]
        public int[][] ContinentRectangle { get; set; } = new int[0][];

        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, PointOfInterest> PointsOfInterest { get; set; } = new Dictionary<int, PointOfInterest>(0);
        
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, MapTask> Tasks { get; set; } = new Dictionary<int, MapTask>(0);

        [JsonProperty(Required = Required.Always)]
        public SkillChallenge[] SkillChallenges { get; set; } = new SkillChallenge[0];

        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, MapSector> Sectors { get; set; } = new Dictionary<int, MapSector>(0);

        [JsonProperty(Required = Required.Always)]
        public Adventure[] Adventures { get; set; } = new Adventure[0];

        [JsonProperty(Required = Required.Always)]
        public MasteryPoint[] MasteryPoints { get; set; } = new MasteryPoint[0];

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public GodShrine[]? GodShrines { get; set; }
    }
}
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

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MinLevel { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MaxLevel { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int DefaultFloor { get; set; }

        [CanBeNull]
        [JsonProperty("label_coord", Required = Required.DisallowNull)]
        public int[] LabelCoordinates { get; set; }

        [NotNull]
        [JsonProperty("map_rect", Required = Required.Always)]
        public int[][] MapRectangle { get; set; }

        [NotNull]
        [JsonProperty("continent_rect", Required = Required.Always)]
        public int[][] ContinentRectangle { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, PointOfInterest> PointsOfInterest { get; set; }
        
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, MapTask> Tasks { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public SkillChallenge[] SkillChallenges { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, MapSector> Sectors { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Adventure[] Adventures { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public MasteryPoint[] MasteryPoints { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public GodShrine[] GodShrines { get; set; }
    }
}
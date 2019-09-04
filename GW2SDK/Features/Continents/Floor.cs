using System.Collections.Generic;
using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Continents.Impl;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class Floor
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [NotNull]
        [JsonProperty("texture_dims", Required = Required.Always)]
        public int[] TextureDimensions { get; set; }

        [CanBeNull]
        [ItemNotNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public int[][] ClampedView { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, Region> Regions { get; set; }
    }

    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class Region
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty("label_coord", Required = Required.Always)]
        public int[] LabelCoordinates { get; set; }

        [NotNull]
        [JsonProperty("continent_rect", Required = Required.Always)]
        public int[][] ContinentRectangle { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, Map> Maps { get; set; }
    }

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

    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(PointOfInterestDiscriminatorOptions))]
    [DataTransferObject(RootObject = false)]
    public class PointOfInterest
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Floor { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public int[] Coordinates { get; set; }
        
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; }
    }

    [DebuggerDisplay("{Name,nq}")]
    public sealed class Landmark : PointOfInterest
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name,nq}")]
    public sealed class Waypoint : PointOfInterest
    {
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name,nq}")]
    public sealed class Vista : PointOfInterest
    {
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name,nq}")]
    public sealed class UnlockerPointOfInterest : PointOfInterest
    {
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Icon { get; set; }
    }

    [PublicAPI]
    [DebuggerDisplay("{Objective,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class MapTask
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Objective { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; }
        
        [NotNull]
        [ItemNotNull]
        [JsonProperty("bounds", Required = Required.Always)]
        public double[][] Boundaries { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; }
    }

    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class SkillChallenge
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; }
    }

    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class MapSector
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Name { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; }

        [NotNull]
        [ItemNotNull]
        [JsonProperty("bounds", Required = Required.Always)]
        public double[][] Boundaries { get; set; }
        
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; }
    }

    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class Adventure
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; }
    }

    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class MasteryPoint
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public MasteryRegionName Region { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; }
    }

    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class GodShrine
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("poi_id", Required = Required.Always)]
        public int PointOfInterestId { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string NameContested { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Icon { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string IconContested { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; }
    }
}

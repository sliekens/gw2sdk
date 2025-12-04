using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Worlds;

/// <summary>The world region where a world is located.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(WorldRegionJsonConverter))]
public enum WorldRegion
{
    /// <summary>The world region is missing or unknown.</summary>
    None,

    /// <summary>The world is associated with North America.</summary>
    NorthAmerica,

    /// <summary>The world is associated with Europe.</summary>
    Europe
}

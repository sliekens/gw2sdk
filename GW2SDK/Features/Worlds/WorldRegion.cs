using System.ComponentModel;

namespace GuildWars2.Worlds;

/// <summary>The world region where a world is located.</summary>
[PublicAPI]
[DefaultValue(None)]
public enum WorldRegion
{
    /// <summary>The world region is missing or unknown.</summary>
    None,

    /// <summary>The world is associated with North America.</summary>
    NorthAmerica,

    /// <summary>The world is associated with Europe.</summary>
    Europe
}

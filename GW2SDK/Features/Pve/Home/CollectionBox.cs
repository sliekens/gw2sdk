using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Pve.Home;

/// <summary>The homestead collection boxes.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(CollectionBoxJsonConverter))]
public enum CollectionBox
{
    /// <summary>No specific collection box or unknown collection box.</summary>
    None,

    /// <summary>The harvesting collection box.</summary>
    Harvesting,

    /// <summary>The logging collection box.</summary>
    Logging,

    /// <summary>The mining collection box.</summary>
    Mining
}

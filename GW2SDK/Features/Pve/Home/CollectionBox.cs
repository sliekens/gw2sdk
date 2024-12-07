using System.Text.Json.Serialization;

namespace GuildWars2.Pve.Home;

/// <summary>The homestead collection boxes.</summary>
[PublicAPI]
[JsonConverter(typeof(CollectionBoxJsonConverter))]
public enum CollectionBox
{
    /// <summary>The harvesting collection box.</summary>
    Harvesting = 1,

    /// <summary>The logging collection box.</summary>
    Logging,

    /// <summary>The mining collection box.</summary>
    Mining
}

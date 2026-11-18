using System.Text.Json.Serialization;

namespace GuildWars2.Exploration.PointsOfInterest;

/// <summary>Information about a point of interest which is locked by default, like a dungeon entrance.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record RequiresUnlockPointOfInterest : PointOfInterest
{
    /// <summary>The URL of the point of interest icon.</summary>
    public required Uri IconUrl { get; init; }
}

using System.Text.Json.Serialization;

namespace GuildWars2.Exploration.PointsOfInterest;

/// <summary>Information about a waypoint.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Waypoint : PointOfInterest;

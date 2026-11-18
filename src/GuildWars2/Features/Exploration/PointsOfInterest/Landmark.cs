using System.Text.Json.Serialization;

namespace GuildWars2.Exploration.PointsOfInterest;

/// <summary>Information about a landmark, which indicates a notable location.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Landmark : PointOfInterest;
